using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApplication1.Migrations
{
    public partial class GetChartStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE PROCEDURE GetWeeklyAverageTemperatures
    @deviceId int
AS
BEGIN
  DECLARE @min int, @max int, @interval int;
  SELECT @min = MIN(Timestamp) FROM Temperature WHERE DeviceId = @deviceId;
  SELECT @max = MAX(Timestamp) FROM Temperature WHERE DeviceId = @deviceId;
  SELECT @interval = (@max - @min) / 51;
  SELECT
    (Timestamp -  @min) / @interval Week,
    AVG(Temp) AvgResult
  FROM Temperature
  WHERE DeviceId = @deviceId
  GROUP BY (Timestamp -  @min) / @interval
  ORDER BY Week
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetWeeklyAverageTemperatures;");
        }
    }
}
