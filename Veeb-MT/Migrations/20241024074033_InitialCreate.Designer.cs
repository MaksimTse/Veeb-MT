﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Veeb_MT.Data;

#nullable disable

namespace Veeb_MT.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241024074033_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Veeb_MT.Models.Kasutaja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Eesnimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parool")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Perenimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Kasutajad");
                });

            modelBuilder.Entity("Veeb_MT.Models.Ostukorv", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("KasutajaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KasutajaId");

                    b.ToTable("Ostukorvid");
                });

            modelBuilder.Entity("Veeb_MT.Models.OstukorvToode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OstukorvId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ToodeId")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OstukorvId");

                    b.HasIndex("ToodeId");

                    b.ToTable("OstukorvTooted");
                });

            modelBuilder.Entity("Veeb_MT.Models.Toode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Nimi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Tooted");
                });

            modelBuilder.Entity("Veeb_MT.Models.Ostukorv", b =>
                {
                    b.HasOne("Veeb_MT.Models.Kasutaja", "Kasutaja")
                        .WithMany("Orders")
                        .HasForeignKey("KasutajaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kasutaja");
                });

            modelBuilder.Entity("Veeb_MT.Models.OstukorvToode", b =>
                {
                    b.HasOne("Veeb_MT.Models.Ostukorv", "Ostukorv")
                        .WithMany("Items")
                        .HasForeignKey("OstukorvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Veeb_MT.Models.Toode", "Toode")
                        .WithMany("OstukorvTooted")
                        .HasForeignKey("ToodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ostukorv");

                    b.Navigation("Toode");
                });

            modelBuilder.Entity("Veeb_MT.Models.Kasutaja", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Veeb_MT.Models.Ostukorv", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Veeb_MT.Models.Toode", b =>
                {
                    b.Navigation("OstukorvTooted");
                });
#pragma warning restore 612, 618
        }
    }
}
