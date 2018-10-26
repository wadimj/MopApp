using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using Bogus;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    public partial class PopulateDataV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var devices = new List<Device>();

            for (int i = 0; i < 5; i++)
            {
                devices.Add(new Faker<Device>()
                    .RuleFor(o => o.Name, f => f.Hacker.Phrase()));
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
