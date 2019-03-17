﻿// <auto-generated />
using System;
using InGame.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InGame.Infrastructure.Migrations
{
    [DbContext(typeof(InGameContext))]
    [Migration("20190317114151_InitialDbMigration21")]
    partial class InitialDbMigration21
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InGame.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<string>("Description");

                    b.Property<string>("PictureUri");

                    b.Property<string>("Uri");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("InGame.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("PictureUri");

                    b.Property<decimal>("Price");

                    b.Property<int?>("SubCategoryID");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("InGame.Core.Entities.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryID");

                    b.Property<string>("Description");

                    b.Property<string>("SubCategoryName");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("InGame.Core.Entities.Product", b =>
                {
                    b.HasOne("InGame.Core.Entities.SubCategory", "Subcategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryID");
                });

            modelBuilder.Entity("InGame.Core.Entities.SubCategory", b =>
                {
                    b.HasOne("InGame.Core.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryID");
                });
#pragma warning restore 612, 618
        }
    }
}