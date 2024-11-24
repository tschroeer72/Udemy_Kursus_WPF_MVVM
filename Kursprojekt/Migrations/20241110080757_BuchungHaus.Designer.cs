﻿// <auto-generated />
using System;
using Kursprojekt.Datenbank;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kursprojekt.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20241110080757_BuchungHaus")]
    partial class BuchungHaus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Kursprojekt.Datenbank.Models.AppUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "t.schroeer@web.de",
                            FirstName = "Thorsten",
                            LastName = "Schröer",
                            Password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=",
                            RoleID = 1
                        });
                });

            modelBuilder.Entity("Kursprojekt.Datenbank.Models.Buchung", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Bearbeiter")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BesichtigungDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("BesichtigungZeit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BuchungDatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("HausID")
                        .HasColumnType("int");

                    b.Property<string>("KundenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KundenTelefonNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("HausID");

                    b.ToTable("Buchungen");
                });

            modelBuilder.Entity("Kursprojekt.Datenbank.Models.Haus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Anschrift")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuftragID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Bild")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IstReserviert")
                        .HasColumnType("bit");

                    b.Property<bool>("IstVerkauft")
                        .HasColumnType("bit");

                    b.Property<double>("Preis")
                        .HasColumnType("float");

                    b.Property<DateTime>("VerkaufDatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("ZimmerAnzahl")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Häuser");
                });

            modelBuilder.Entity("Kursprojekt.Datenbank.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            RoleName = "Admin"
                        });
                });

            modelBuilder.Entity("Kursprojekt.Datenbank.Models.AppUser", b =>
                {
                    b.HasOne("Kursprojekt.Datenbank.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Kursprojekt.Datenbank.Models.Buchung", b =>
                {
                    b.HasOne("Kursprojekt.Datenbank.Models.Haus", "Haus")
                        .WithMany()
                        .HasForeignKey("HausID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Haus");
                });
#pragma warning restore 612, 618
        }
    }
}