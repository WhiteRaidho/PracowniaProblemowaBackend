﻿// <auto-generated />
using System;
using FitKidCateringApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FitKidCateringApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201125145329_005_orders")]
    partial class _005_orders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FitKidCateringApp.Models.Children.Child", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<long>("InstitutionId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<long>("ParentId");

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.HasIndex("ParentId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Core.CorePermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId");

                    b.Property<string>("AuthorType");

                    b.Property<string>("PermissionsJson");

                    b.Property<string>("PermissionsType");

                    b.Property<long?>("ResourceId");

                    b.Property<string>("ResourceType");

                    b.HasKey("Id");

                    b.ToTable("CorePermissions");
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Core.CoreRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CoreRoles");
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Core.CoreUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RefreshToken");

                    b.Property<string>("RolesJson");

                    b.Property<string>("Token");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CoreUsers");
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Institutions.Institution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.Property<long>("OwnerId");

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Offers.Offer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("DayOfWeek");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Orders.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ChildId");

                    b.Property<string>("OffersJson");

                    b.Property<Guid>("PublicId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Children.Child", b =>
                {
                    b.HasOne("FitKidCateringApp.Models.Institutions.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FitKidCateringApp.Models.Core.CoreUser", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Institutions.Institution", b =>
                {
                    b.HasOne("FitKidCateringApp.Models.Core.CoreUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FitKidCateringApp.Models.Orders.Order", b =>
                {
                    b.HasOne("FitKidCateringApp.Models.Children.Child", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
