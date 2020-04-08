namespace Geo.Service.Data
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class GeoDbContext : DbContext
	{
		public GeoDbContext(DbContextOptions<GeoDbContext> options) : base(options)
		{
			States.AsNoTracking();
			Counties.AsNoTracking();
			ZipCodes.AsNoTracking();
			Cities.AsNoTracking();
		}

		public DbSet<State.State> States { get; set; }
		public DbSet<County.County> Counties { get; set; }
		public DbSet<ZipCode.ZipCode> ZipCodes { get; set; }
		public DbSet<City.City> Cities { get; set; }
		//public DbSet<CityAlias.CityAlias> CityAliases { get; set; }
		//public DbSet<CoreBasedStatisticalArea> CoreBasedStatisticalAreas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			EntityTypeBuilder<State.State> builderState = modelBuilder.Entity<State.State>();
			builderState.HasKey(_ => _.StateId);
			builderState.Property(_ => _.StateIso).HasMaxLength(2).IsRequired();
			builderState.HasAlternateKey(_ => _.StateIso);
			builderState.HasIndex(_ => _.StateIso).IsUnique();
			builderState.Property(_ => _.Name).HasMaxLength(80).IsRequired();
			builderState.Property(_ => _.Fips).HasMaxLength(2).IsRequired();
			builderState.HasAlternateKey(_ => _.Fips);
			builderState.HasIndex(_ => _.Fips).IsUnique();
			builderState.Property(_ => _.IsTerritory).HasDefaultValue(false).IsRequired();
			builderState.Property(_ => _.CountryIso).HasMaxLength(2).IsRequired();
			builderState.Ignore(_ => _.Country);

			EntityTypeBuilder<County.County> builderCounty = modelBuilder.Entity<County.County>();
			builderCounty.HasKey(_ => _.CountyId);
			builderCounty.Property(_ => _.Name).HasMaxLength(100).IsRequired();
			builderCounty.Property(_ => _.Fips).HasMaxLength(3).IsRequired();
			builderCounty.Property(_ => _.FullFips).HasMaxLength(5).IsRequired();
			builderCounty.HasAlternateKey(_ => _.FullFips);
			builderCounty.Property(_ => _.StateIso).HasMaxLength(2).IsRequired();
			builderCounty.HasOne(_ => _.State).WithMany();
			builderCounty.Property(_ => _.CountryIso).HasMaxLength(2).IsRequired();
			builderCounty.Ignore(_ => _.Country);

			EntityTypeBuilder<ZipCode.ZipCode> builderZips = modelBuilder.Entity<ZipCode.ZipCode>();
			builderZips.HasKey(_ => _.ZipCodeId);
			builderZips.Property(_ => _.ZipCode5).HasMaxLength(5).IsRequired();
			builderZips.Property(_ => _.Latitude).HasColumnType("decimal(11,8)").IsRequired();
			builderZips.Property(_ => _.Longitude).HasColumnType("decimal(11,8)").IsRequired();
			builderZips.Property(_ => _.CountryIso).HasMaxLength(2).IsRequired();
			builderZips.Ignore(_ => _.Country);

			EntityTypeBuilder<ZipCode.ZipCodeState> builderZipStates = modelBuilder.Entity<ZipCode.ZipCodeState>();
			builderZipStates.HasKey(_ => _.ZipCodeStateId);
			builderZipStates.HasOne(_ => _.State).WithMany(_ => _.ZipCodes);
			builderZipStates.HasOne(_ => _.ZipCode).WithMany(_ => _.States);

			EntityTypeBuilder<ZipCode.ZipCodeCity> builderZipCities = modelBuilder.Entity<ZipCode.ZipCodeCity>();
			builderZipCities.HasKey(_ => _.ZipCodeCityId);
			builderZipCities.HasOne(_ => _.City).WithMany(_ => _.ZipCodes);
			builderZipCities.HasOne(_ => _.ZipCode).WithMany(_ => _.Cities);

			EntityTypeBuilder<ZipCode.ZipCodeCounty> builderZipCounties = modelBuilder.Entity<ZipCode.ZipCodeCounty>();
			builderZipCounties.HasKey(_ => _.ZipCodeCountyId);
			builderZipCounties.HasOne(_ => _.County).WithMany(_ => _.ZipCodes);
			builderZipCounties.HasOne(_ => _.ZipCode).WithMany(_ => _.Counties);

			EntityTypeBuilder<City.City> builderCity = modelBuilder.Entity<City.City>();
			builderCity.HasKey(_ => _.CityId);
			builderCity.Property(_ => _.Id).HasMaxLength(100).IsRequired();
			builderCity.HasAlternateKey(_ => _.Id);
			builderCity.HasIndex(_ => _.Id).IsUnique();
			builderCity.Property(_ => _.Name).HasMaxLength(100).IsRequired();
			builderCity.Property(_ => _.StateIso).HasMaxLength(2).IsRequired();
			builderCity.HasOne(_ => _.State).WithMany();
			builderCity.Property(_ => _.CountryIso).HasMaxLength(2).IsRequired();
			builderCity.Ignore(_ => _.Country);

			EntityTypeBuilder<City.CityCounty> builderCityCounties = modelBuilder.Entity<City.CityCounty>();
			builderCityCounties.HasKey(_ => _.CityCountyId);
			builderCityCounties.HasOne(_ => _.County).WithMany(_ => _.Cities);
			builderCityCounties.HasOne(_ => _.City).WithMany(_ => _.Counties);

			//EntityTypeBuilder<CityAlias.CityAlias> builderCityAlias = modelBuilder.Entity<CityAlias.CityAlias>();
			//builderCityAlias.HasKey(_ => _.CityAliasId);
			//builderCityAlias.Property(_ => _.CityId).IsRequired();
			//builderCityAlias.Property(_ => _.Abbreviation).HasMaxLength(35);
			//builderCityAlias.Property(_ => _.Alias).HasMaxLength(35).IsRequired();

			//EntityTypeBuilder<CoreBasedStatisticalArea> builderCbsa = modelBuilder.Entity<CoreBasedStatisticalArea>();
			//builderCbsa.HasKey(_ => _.CoreBasedStatisticalAreaKey);
			//builderCbsa.Property(_ => _.Div);
			//builderCbsa.Property(_ => _.Title).HasMaxLength(80).IsRequired();
		}

		//public static ILoggerProvider MyLoggingProvider => new DebugLoggerProvider();

		//public static readonly ILoggerFactory MyLoggerFactory =
		//	LoggerFactory.Create(builder => builder.AddProvider(MyLoggingProvider));

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseLoggerFactory(MyLoggerFactory);
		//	base.OnConfiguring(optionsBuilder);
		//}
	}
}