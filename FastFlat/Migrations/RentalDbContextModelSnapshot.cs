﻿// <auto-generated />
using System;
using FastFlat.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FastFlat.Migrations
{
    [DbContext(typeof(RentalDbContext))]
    partial class RentalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("FastFlat.Models.AmenityModel", b =>
                {
                    b.Property<int>("AmenityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AmenityDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("AmenityLogo")
                        .HasColumnType("TEXT");

                    b.Property<string>("AmenityName")
                        .HasColumnType("TEXT");

                    b.HasKey("AmenityId");

                    b.ToTable("AmenityModel");
                });

            modelBuilder.Entity("FastFlat.Models.BookingModel", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ListningId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecialRequests")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("renterModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BookingId");

                    b.HasIndex("ListningId");

                    b.HasIndex("UserModelId");

                    b.HasIndex("renterModelId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("FastFlat.Models.ContryModel", b =>
                {
                    b.Property<int>("ContryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Contryname")
                        .HasColumnType("TEXT");

                    b.HasKey("ContryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FastFlat.Models.LandlordModel", b =>
                {
                    b.Property<int>("landlordModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("rating")
                        .HasColumnType("REAL");

                    b.HasKey("landlordModelId");

                    b.ToTable("Landlord");
                });

            modelBuilder.Entity("FastFlat.Models.ListningAmenity", b =>
                {
                    b.Property<int>("ListningAmenityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmenityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ListningId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ListningAmenityId");

                    b.HasIndex("AmenityId");

                    b.HasIndex("ListningId");

                    b.ToTable("ListningAmenity");
                });

            modelBuilder.Entity("FastFlat.Models.ListningModel", b =>
                {
                    b.Property<int>("ListningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("FromDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListningAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListningDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListningImageURL")
                        .HasColumnType("TEXT");

                    b.Property<string>("ListningName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("ListningPrice")
                        .HasColumnType("REAL");

                    b.Property<int?>("LocationID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("NoOfBeds")
                        .HasColumnType("INTEGER");

                    b.Property<float?>("Rating")
                        .HasColumnType("REAL");

                    b.Property<int?>("SquareMeter")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ToDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ListningId");

                    b.HasIndex("LocationID");

                    b.HasIndex("UserId");

                    b.HasIndex("UserModelId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("FastFlat.Models.LocationModel", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LocationID");

                    b.ToTable("LocationModel");
                });

            modelBuilder.Entity("FastFlat.Models.RenterModel", b =>
                {
                    b.Property<int>("renterModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("renterModelId");

                    b.ToTable("Renters");
                });

            modelBuilder.Entity("FastFlat.Models.UserModel", b =>
                {
                    b.Property<int>("UserModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Phone")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.Property<int?>("landlordModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserModelId");

                    b.HasIndex("landlordModelId");

                    b.ToTable("UserModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("UsernameChangeLimit")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("FastFlat.Models.BookingModel", b =>
                {
                    b.HasOne("FastFlat.Models.ListningModel", "Listning")
                        .WithMany("bookings")
                        .HasForeignKey("ListningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FastFlat.Models.UserModel", null)
                        .WithMany("Bookings")
                        .HasForeignKey("UserModelId");

                    b.HasOne("FastFlat.Models.RenterModel", null)
                        .WithMany("bookings")
                        .HasForeignKey("renterModelId");

                    b.Navigation("Listning");
                });

            modelBuilder.Entity("FastFlat.Models.ListningAmenity", b =>
                {
                    b.HasOne("FastFlat.Models.AmenityModel", "Amenity")
                        .WithMany("ListningAmenities")
                        .HasForeignKey("AmenityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FastFlat.Models.ListningModel", "Listning")
                        .WithMany("ListningAmenities")
                        .HasForeignKey("ListningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amenity");

                    b.Navigation("Listning");
                });

            modelBuilder.Entity("FastFlat.Models.ListningModel", b =>
                {
                    b.HasOne("FastFlat.Models.LocationModel", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID");

                    b.HasOne("ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("FastFlat.Models.UserModel", null)
                        .WithMany("Rentals")
                        .HasForeignKey("UserModelId");

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FastFlat.Models.UserModel", b =>
                {
                    b.HasOne("FastFlat.Models.LandlordModel", null)
                        .WithMany("users")
                        .HasForeignKey("landlordModelId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FastFlat.Models.AmenityModel", b =>
                {
                    b.Navigation("ListningAmenities");
                });

            modelBuilder.Entity("FastFlat.Models.LandlordModel", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("FastFlat.Models.ListningModel", b =>
                {
                    b.Navigation("ListningAmenities");

                    b.Navigation("bookings");
                });

            modelBuilder.Entity("FastFlat.Models.RenterModel", b =>
                {
                    b.Navigation("bookings");
                });

            modelBuilder.Entity("FastFlat.Models.UserModel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
