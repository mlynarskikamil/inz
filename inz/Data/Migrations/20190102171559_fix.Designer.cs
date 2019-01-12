﻿// <auto-generated />
using inz.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace inz.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190102171559_fix")]
    partial class fix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("inz.Models.Album", b =>
                {
                    b.Property<int>("ID_Album")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImgAlbumUrl");

                    b.Property<string>("InfoAlbum");

                    b.Property<string>("Name_Album");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.HasKey("ID_Album");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("inz.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("inz.Models.Artist", b =>
                {
                    b.Property<int>("ID_Artist")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("ImgArtistUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Name_Artist");

                    b.Property<string>("Surname");

                    b.HasKey("ID_Artist");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("inz.Models.Opinion", b =>
                {
                    b.Property<int>("ID_Opinion")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<bool>("Direction");

                    b.Property<int?>("ID_Song");

                    b.Property<string>("Id");

                    b.HasKey("ID_Opinion");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ID_Song")
                        .IsUnique()
                        .HasFilter("[ID_Song] IS NOT NULL");

                    b.ToTable("Opinion");
                });

            modelBuilder.Entity("inz.Models.Producer", b =>
                {
                    b.Property<int>("ID_Producer")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name_Producer");

                    b.HasKey("ID_Producer");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("inz.Models.Song", b =>
                {
                    b.Property<int>("ID_Song")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ID_Album");

                    b.Property<int?>("ID_Artist");

                    b.Property<int?>("ID_Producer");

                    b.Property<int>("Like");

                    b.Property<DateTime>("ReleaseSong")
                        .HasColumnType("date");

                    b.Property<string>("Title");

                    b.Property<int>("Unlike");

                    b.Property<string>("UrlAzure");

                    b.Property<string>("VideoUrl");

                    b.HasKey("ID_Song");

                    b.HasIndex("ID_Album");

                    b.HasIndex("ID_Artist");

                    b.HasIndex("ID_Producer");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("inz.Models.Opinion", b =>
                {
                    b.HasOne("inz.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("inz.Models.Song", "Song")
                        .WithOne("Opinion")
                        .HasForeignKey("inz.Models.Opinion", "ID_Song");
                });

            modelBuilder.Entity("inz.Models.Song", b =>
                {
                    b.HasOne("inz.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("ID_Album");

                    b.HasOne("inz.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ID_Artist");

                    b.HasOne("inz.Models.Producer", "Producer")
                        .WithMany("Songs")
                        .HasForeignKey("ID_Producer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("inz.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("inz.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("inz.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("inz.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
