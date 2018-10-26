using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebApplication1.Migrations
{
    public partial class PopulateTemparatureData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Fixtures/MockTemperatures.sql");
            string sql = File.ReadAllText(sqlFile);
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Temperature");
        }
    }
}
