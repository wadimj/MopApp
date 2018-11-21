using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class GetDevicesStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE PROCEDURE GetDeviceAverage
AS
BEGIN
  SELECT D.*, AVG(T.Temp) Average
  FROM Temperature T
  INNER JOIN Devices D on T.DeviceId = D.Id
  GROUP BY D.Id, D.Name
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetDeviceAverage;");
        }
    }
}
