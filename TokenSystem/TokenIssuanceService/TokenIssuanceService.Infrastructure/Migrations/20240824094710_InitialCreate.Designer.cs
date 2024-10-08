﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TokenIssuanceService.Infrastructure.Data;

#nullable disable

namespace TokenIssuanceService.Infrastructure.Migrations
{
    [DbContext(typeof(TokenDbContext))]
    [Migration("20240824094710_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TokenIssuanceService.Domain.Entities.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("IssueDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ServiceCategory")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceCategory");

                    b.ToTable("Tokens");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientName = "John Doe",
                            IssueDateTime = new DateTime(2024, 8, 24, 15, 17, 10, 128, DateTimeKind.Local).AddTicks(9629),
                            ServiceCategory = 0,
                            Status = "Pending"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
