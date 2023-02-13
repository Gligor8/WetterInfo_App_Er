﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WetterInfo.DataAccess;

namespace WetterInfo.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("WetterInfo.Models.ResultView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AirPressure")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<int>("CloudCondition")
                        .HasColumnType("integer");

                    b.Property<double>("CurrentTemp")
                        .HasColumnType("double precision");

                    b.Property<int>("Humidity")
                        .HasColumnType("integer");

                    b.Property<double>("TempMax")
                        .HasColumnType("double precision");

                    b.Property<double>("TempMin")
                        .HasColumnType("double precision");

                    b.Property<int>("WindDirection")
                        .HasColumnType("integer");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Responses");
                });
#pragma warning restore 612, 618
        }
    }
}
