﻿// <auto-generated />
using System;
using Denggaopan.WorkerServiceDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Denggaopan.WorkerServiceDemo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200619062501_ModifySystemLog2")]
    partial class ModifySystemLog2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Denggaopan.WorkerServiceDemo.Entities.SystemLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Module")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("SystemLog");
                });
#pragma warning restore 612, 618
        }
    }
}
