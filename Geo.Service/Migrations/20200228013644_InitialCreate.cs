using Microsoft.EntityFrameworkCore.Migrations;

namespace Geo.Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateIso = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Fips = table.Column<string>(maxLength: 2, nullable: false),
                    IsTerritory = table.Column<bool>(nullable: false, defaultValue: false),
                    CountryIso = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                    table.UniqueConstraint("AK_States_Fips", x => x.Fips);
                    table.UniqueConstraint("AK_States_StateIso", x => x.StateIso);
                });

            migrationBuilder.CreateTable(
                name: "ZipCodes",
                columns: table => new
                {
                    ZipCodeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipCode5 = table.Column<string>(maxLength: 5, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    CountryIso = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodes", x => x.ZipCodeId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    StateIso = table.Column<string>(maxLength: 2, nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    CountryIso = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                    table.UniqueConstraint("AK_Cities_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    CountyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Fips = table.Column<string>(maxLength: 3, nullable: false),
                    FullFips = table.Column<string>(maxLength: 5, nullable: false),
                    StateIso = table.Column<string>(maxLength: 2, nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    CountryIso = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.CountyId);
                    table.UniqueConstraint("AK_Counties_FullFips", x => x.FullFips);
                    table.ForeignKey(
                        name: "FK_Counties_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZipCodeState",
                columns: table => new
                {
                    ZipCodeStateId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(nullable: true),
                    ZipCodeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodeState", x => x.ZipCodeStateId);
                    table.ForeignKey(
                        name: "FK_ZipCodeState_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZipCodeState_ZipCodes_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "ZipCodes",
                        principalColumn: "ZipCodeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZipCodeCity",
                columns: table => new
                {
                    ZipCodeCityId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<long>(nullable: true),
                    ZipCodeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodeCity", x => x.ZipCodeCityId);
                    table.ForeignKey(
                        name: "FK_ZipCodeCity_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZipCodeCity_ZipCodes_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "ZipCodes",
                        principalColumn: "ZipCodeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityCounty",
                columns: table => new
                {
                    CityCountyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyId = table.Column<int>(nullable: true),
                    CityId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityCounty", x => x.CityCountyId);
                    table.ForeignKey(
                        name: "FK_CityCounty_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityCounty_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "CountyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZipCodeCounty",
                columns: table => new
                {
                    ZipCodeCountyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyId = table.Column<int>(nullable: true),
                    ZipCodeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodeCounty", x => x.ZipCodeCountyId);
                    table.ForeignKey(
                        name: "FK_ZipCodeCounty_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "CountyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZipCodeCounty_ZipCodes_ZipCodeId",
                        column: x => x.ZipCodeId,
                        principalTable: "ZipCodes",
                        principalColumn: "ZipCodeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Id",
                table: "Cities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CityCounty_CityId",
                table: "CityCounty",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CityCounty_CountyId",
                table: "CityCounty",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Counties_StateId",
                table: "Counties",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_States_Fips",
                table: "States",
                column: "Fips",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_StateIso",
                table: "States",
                column: "StateIso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZipCodeCity_CityId",
                table: "ZipCodeCity",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ZipCodeCity_ZipCodeId",
                table: "ZipCodeCity",
                column: "ZipCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ZipCodeCounty_CountyId",
                table: "ZipCodeCounty",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_ZipCodeCounty_ZipCodeId",
                table: "ZipCodeCounty",
                column: "ZipCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ZipCodeState_StateId",
                table: "ZipCodeState",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ZipCodeState_ZipCodeId",
                table: "ZipCodeState",
                column: "ZipCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityCounty");

            migrationBuilder.DropTable(
                name: "ZipCodeCity");

            migrationBuilder.DropTable(
                name: "ZipCodeCounty");

            migrationBuilder.DropTable(
                name: "ZipCodeState");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "ZipCodes");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
