namespace Geo.Service
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using System.Runtime.InteropServices;
	using System.Text;
	using Data;
	using Data.City;
	using Data.County;
	using Data.State;
	using Data.ZipCode;
	using Inflection.Common;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Diagnostics;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.WebUtilities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;
	using Model.Exceptions;
	using NSwag;

	public class Startup
	{
		private const string InfoVersion = "v1";
		private const string InfoTitle = "Geo Service";
		private const string InfoDescription = "Returns information about geographical locations";

		private ILogger Logger { get; set; }
		private IWebHostEnvironment Env { get; }
		private IConfiguration Configuration { get; }

		public Startup(IWebHostEnvironment env, IConfiguration configuration)
		{
			Env = env;
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			string connectionString = GetConnectionStringFromConfig();
			services.AddDbContextPool<GeoDbContext>(options => options.UseSqlServer(connectionString));

			services.AddScoped<ICountyDao, CountyDao>();
			services.AddScoped<IStateDao, StateDao>();
			services.AddScoped<IZipCodeDao, ZipCodeDao>();
			services.AddScoped<ICityDao, CityDao>();

			services.AddControllers();

			// NSwag
			services.AddOpenApiDocument();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			Logger = logger;
			Logger.LogCritical($"ENV: {Env.EnvironmentName}");
			ReportLogLevel();

			SetupExceptionHandler(app);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

			// Use Swagger UI (this is also the API homepage)
			SetupSwaggerUi(app, env, OpenApiSchema.Http);
		}

		/// <summary>
		/// Reports the log level.
		/// </summary>
		private void ReportLogLevel()
		{
			IEnumerable<LogLevel> logLevels = Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>().Where(_ => _ != LogLevel.None && Logger.IsEnabled(_));
			LogLevel logLevelMin = logLevels.Min();
			LogLevel logLevelMax = logLevels.Max();
			Console.WriteLine($"Log Level range: {logLevelMin} -> {logLevelMax}");
		}

		private void SetupSwaggerUi(IApplicationBuilder app, IWebHostEnvironment env, OpenApiSchema openApiSchema)
		{
			app.UseOpenApi(settings =>
			{
				settings.PostProcess = (document, httpRequest) =>
				{
					document.Info.Version = InfoVersion;
					document.Info.Title = $"{InfoTitle} - ({env.EnvironmentName})";
					document.Info.Description = InfoDescription;
					document.Schemes.Clear();
					document.Schemes.Add(openApiSchema);
				};
			});
			app.UseSwaggerUi3();
		}

		/// <summary>
		/// Setup a global exception handler.
		/// </summary>
		/// <param name="app">The application.</param>
		private void SetupExceptionHandler(IApplicationBuilder app)
		{
			app.UseExceptionHandler(errorApp =>
			{
				errorApp.Run(async context =>
				{
					IExceptionHandlerFeature errorFeature = context.Features.Get<IExceptionHandlerFeature>();
					Exception exception = errorFeature.Error.GetBaseException();

					Logger.LogError(exception, "Exception caught in Default Exception Handler");

					HttpStatusCode code = HttpStatusCode.InternalServerError;

					//HttpStatusCode code = exception is HttpTranslatableException ex
					//	? ex.HttpStatusCode
					//	: HttpStatusCode.InternalServerError;

					HttpRequest request = context.Request;

					// All exceptions streamed to the body as this type
					Uri url = new Uri($"{request.Scheme}://{request.Host}{request.Path.Value}");
					GeoServiceException error = new GeoServiceException(HttpStatusCode.InternalServerError, exception.Message, url, exception.StackTrace);

					HttpResponse response = context.Response;
					response.StatusCode = (int)code;
					response.ContentType = "application/json";

					await using HttpResponseStreamWriter writer = new HttpResponseStreamWriter(response.Body, Encoding.UTF8);
					await writer.WriteAsync(error.ToJson());
				});
			});
		}

		private string GetConnectionStringFromConfig()
		{
			return Configuration.GetConnectionString("GeoDb");
		}
	}
}
