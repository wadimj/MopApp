﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(MopContext))]
    partial class TemperatureContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Models.ChartData", b =>
                {
                    b.Property<int>("Week")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AvgResult");

                    b.HasKey("Week");

                    b.ToTable("ChartDatas");
                });

            modelBuilder.Entity("WebApplication1.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Average");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("WebApplication1.Models.Temperature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DeviceId");

                    b.Property<double>("Temp");

                    b.Property<int>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Temperature");
                });

            modelBuilder.Entity("WebApplication1.Models.Temperature", b =>
                {
                    b.HasOne("WebApplication1.Models.Device", "Device")
                        .WithMany("Temperatures")
                        .HasForeignKey("DeviceId");
                });
#pragma warning restore 612, 618
        }
    }
}
