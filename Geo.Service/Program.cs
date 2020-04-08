namespace Geo.Service
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Serilog;
	using Serilog.Formatting.Json;
	using VaultSharp;
	using VaultSharp.Config;
	using VaultSharp.V1.AuthMethods;
	using VaultSharp.V1.AuthMethods.Token;
	using VaultSharp.V1.Commons;

	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host
				.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((context, builder) =>
				{
					// build a throw-away config just to get the vault params
					IConfigurationRoot builtConfig = builder.Build();

					string vaultUrl = builtConfig["vault_url"];
					string vaultToken = builtConfig["vault_token"];

					// This may be FALSE if we are in development env using the local appsettings file
					if (!string.IsNullOrWhiteSpace(vaultUrl) && !string.IsNullOrWhiteSpace(vaultToken))
					{
						// same as vault path name
						string vaultAppName = "geo_service";

						IAuthMethodInfo authMethod = new TokenAuthMethodInfo(vaultToken);
						VaultClientSettings vaultClientSettings = new VaultClientSettings(vaultUrl, authMethod);
						IVaultClient vaultClient = new VaultClient(vaultClientSettings);

						// renew the token - why not? this just keeps it active.
						Task.Run(async () =>
						{
							AuthInfo result = await vaultClient.V1.Auth.Token.RenewSelfAsync();
							Console.WriteLine($"Token renewed for {new TimeSpan(0, 0, result.LeaseDurationSeconds).Days} more days.");
						});

						string envName = context.HostingEnvironment.EnvironmentName.ToLower();

						// Add HashiCorp Vault as a Formal Configuration Provider
						builder.AddHashiCorpVault(vaultClient, $"apps/{vaultAppName}/{envName}");
					}
				})
				.UseSerilog((context, loggerConfiguration) =>
				{
					loggerConfiguration.ReadFrom.Configuration(context.Configuration);
					loggerConfiguration.Enrich.FromLogContext();
					loggerConfiguration.WriteTo.Console(new JsonFormatter(renderMessage: true));
				})
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		}
	}
}