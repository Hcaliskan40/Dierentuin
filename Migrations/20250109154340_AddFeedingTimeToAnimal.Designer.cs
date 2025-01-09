﻿// <auto-generated />
using Dierentuin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dierentuin.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250109154340_AddFeedingTimeToAnimal")]
    partial class AddFeedingTimeToAnimal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Dierentuin.Models.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActivityPattern")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DietaryClass")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FeedingTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PredatorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SecurityRequirement")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Size")
                        .HasColumnType("INTEGER");

                    b.Property<double>("SpaceRequirement")
                        .HasColumnType("REAL");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SunriseAction")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SunsetAction")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PredatorId");

                    b.ToTable("Animals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActivityPattern = 0,
                            CategoryId = 1,
                            DietaryClass = 0,
                            FeedingTime = 0,
                            Name = "Lion",
                            SecurityRequirement = 2,
                            Size = 4,
                            SpaceRequirement = 12.5,
                            Species = "Panthera leo",
                            SunriseAction = 0,
                            SunsetAction = 1
                        },
                        new
                        {
                            Id = 2,
                            ActivityPattern = 1,
                            CategoryId = 2,
                            DietaryClass = 0,
                            FeedingTime = 1,
                            Name = "Python",
                            SecurityRequirement = 1,
                            Size = 3,
                            SpaceRequirement = 8.0,
                            Species = "Python regius",
                            SunriseAction = 1,
                            SunsetAction = 0
                        },
                        new
                        {
                            Id = 3,
                            ActivityPattern = 0,
                            CategoryId = 1,
                            DietaryClass = 1,
                            FeedingTime = 0,
                            Name = "Deer",
                            SecurityRequirement = 0,
                            Size = 3,
                            SpaceRequirement = 10.0,
                            Species = "Cervidae",
                            SunriseAction = 0,
                            SunsetAction = 1
                        },
                        new
                        {
                            Id = 4,
                            ActivityPattern = 1,
                            CategoryId = 1,
                            DietaryClass = 0,
                            FeedingTime = 1,
                            Name = "Tiger",
                            PredatorId = 1,
                            SecurityRequirement = 2,
                            Size = 4,
                            SpaceRequirement = 15.0,
                            Species = "Panthera tigris",
                            SunriseAction = 1,
                            SunsetAction = 0
                        });
                });

            modelBuilder.Entity("Dierentuin.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            Name = "Mammals"
                        },
                        new
                        {
                            Id = 2,
                            IsActive = true,
                            Name = "Reptiles"
                        });
                });

            modelBuilder.Entity("Dierentuin.Models.Animal", b =>
                {
                    b.HasOne("Dierentuin.Models.Category", "Category")
                        .WithMany("Animals")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Dierentuin.Models.Animal", "Predator")
                        .WithMany("Prey")
                        .HasForeignKey("PredatorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Category");

                    b.Navigation("Predator");
                });

            modelBuilder.Entity("Dierentuin.Models.Animal", b =>
                {
                    b.Navigation("Prey");
                });

            modelBuilder.Entity("Dierentuin.Models.Category", b =>
                {
                    b.Navigation("Animals");
                });
#pragma warning restore 612, 618
        }
    }
}
