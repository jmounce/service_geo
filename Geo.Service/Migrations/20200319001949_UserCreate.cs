using Microsoft.EntityFrameworkCore.Migrations;

namespace Geo.Service.Migrations
{
    public partial class UserCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
	        string sqlUser = @"
				IF NOT EXISTS(SELECT loginname FROM master.dbo.syslogins WHERE name = 'geo_svc' AND dbname = 'master')
				BEGIN
					CREATE LOGIN [geo_svc] WITH PASSWORD=N'P@ssw0rd', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
				END
				GO

				CREATE USER [geo_svc] FOR LOGIN [geo_svc] WITH DEFAULT_SCHEMA = [dbo];
				GO

				GRANT CONNECT TO [geo_svc]
				GO

				EXEC sp_addrolemember [db_datareader], [geo_svc];
				GO

				EXEC sp_addrolemember [db_datawriter], [geo_svc];
				GO

				GRANT EXECUTE TO [geo_svc];
				GO
			";

	        migrationBuilder.Sql(sqlUser);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
	        string sqlUser = @"
				DROP USER [geo_svc];
				GO

				DROP LOGIN [geo_svc];
				GO
			";

	        migrationBuilder.Sql(sqlUser);
        }
    }
}
