﻿// <auto-generated />
using System;
using Geo.Service.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Geo.Service.Migrations
{
    [DbContext(typeof(GeoDbContext))]
    partial class GeoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Geo.Service.Data.City.City", b =>
                {
                    b.Property<long>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("StateIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("CityId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Geo.Service.Data.City.CityCounty", b =>
                {
                    b.Property<long>("CityCountyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CityId")
                        .HasColumnType("bigint");

                    b.Property<int?>("CountyId")
                        .HasColumnType("int");

                    b.HasKey("CityCountyId");

                    b.HasIndex("CityId");

                    b.HasIndex("CountyId");

                    b.ToTable("CityCounty");
                });

            modelBuilder.Entity("Geo.Service.Data.County.County", b =>
                {
                    b.Property<int>("CountyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Fips")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("FullFips")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("StateIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("CountyId");

                    b.HasAlternateKey("FullFips");

                    b.HasIndex("StateId");

                    b.ToTable("Counties");
                });

            modelBuilder.Entity("Geo.Service.Data.State.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Fips")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<bool>("IsTerritory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("StateIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("StateId");

                    b.HasAlternateKey("Fips");

                    b.HasAlternateKey("StateIso");

                    b.HasIndex("Fips")
                        .IsUnique();

                    b.HasIndex("StateIso")
                        .IsUnique();

                    b.ToTable("States");
                });

            modelBuilder.Entity("Geo.Service.Data.ZipCode.ZipCode", b =>
                {
                    b.Property<long>("ZipCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryIso")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.Property<decimal?>("Latitude")
                        .IsRequired()
                        .HasColumnType("decimal(11,8)");

                    b.Property<decimal?>("Longitude")
                        .IsRequired()
                        .HasColumnType("decimal(11,8)");

                    b.Property<string>("ZipCode5")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("ZipCodeId");

                    b.ToTable("ZipCodes");
                });

            modelBuilder.Entity("Geo.Service.Data.ZipCode.ZipCodeCity", b =>
                {
                    b.Property<long>("ZipCodeCityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CityId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ZipCodeId")
                        .HasColumnType("bigint");

                    b.HasKey("ZipCodeCityId");

                    b.HasIndex("CityId");

                    b.HasIndex("ZipCodeId");

                    b.ToTable("ZipCodeCity");
                });

            modelBuilder.Entity("Geo.Service.Data.ZipCode.ZipCodeCounty", b =>
                {
                    b.Property<long>("ZipCodeCountyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountyId")
                        .HasColumnType("int");

                    b.Property<long?>("ZipCodeId")
                        .HasColumnType("bigint");

                    b.HasKey("ZipCodeCountyId");

                    b.HasIndex("CountyId");

                    b.HasIndex("ZipCodeId");

                    b.ToTable("ZipCodeCounty");
                });

            modelBuilder.Entity("Geo.Service.Data.ZipCode.ZipCodeState", b =>
                {
                    b.Property<long>("ZipCodeStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.Property<long?>("ZipCodeId")
                        .HasColumnType("bigint");

                    b.HasKey("ZipCodeStateId");

                    b.HasIndex("StateId");

                    b.HasIndex("ZipCodeId");

                    b.ToTable("ZipCodeState");
                });

            modelBuilder.Entity("Geo.Service.Data.City.City", b =>
                {
                    b.HasOne("Geo.Service.Data.State.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Geo.Service.Data.City.CityCounty", b =>
                {
                    b.HasOne("Geo.Service.Data.City.City", "City")
                        .WithMany("Counties")
                        .HasForeignKey("CityId");

                    b.HasOne("Geo.Service.Data.County.County", "County")
                        .WithMany("Cities")
                        .HasForeignKey("CountyId");
                });

            modelBuilder.Entity("Geo.Service.Data.County.County", b =>
                {
                    b.HasOne("Geo.Service.Data.State.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("Geo.Service.Data.ZipCode.ZipCodeCity", b =>
                {
                    b.HasOne("Geo.Service.Data.City.City", "City")
                        .WithMany("ZipCodes")
                        .HasForeignKey("CityId");

                    b.HasOne("Geo.Service.Data.ZipCode.ZipCode", "ZipCode")
                        .WithMany("Cities")
                        .HasForeignKey("ZipCodeId");
                });

            modelBuilder.Entity("Geo.Service.Data.ZipCode.ZipCodeCounty", b =>
                {
                    b.HasOne("Geo.Service.Data.County.County", "County")
                        .WithMany("ZipCodes")
                        .HasForeignKey("CountyId");

                    b.HasOne("Geo.Service.Data.ZipCode.ZipCode", "ZipCode")
                        .WithMany("Counties")
                        .HasForeignKey("ZipCodeId");
                });

            modelBuilder.Entity("Geo.Service.Data.ZipCode.ZipCodeState", b =>
                {
                    b.HasOne("Geo.Service.Data.State.State", "State")
                        .WithMany("ZipCodes")
                        .HasForeignKey("StateId");

                    b.HasOne("Geo.Service.Data.ZipCode.ZipCode", "ZipCode")
                        .WithMany("States")
                        .HasForeignKey("ZipCodeId");
                });
#pragma warning restore 612, 618
        }
    }
}
