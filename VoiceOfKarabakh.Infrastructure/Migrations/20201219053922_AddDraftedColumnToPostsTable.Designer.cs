﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201219053922_AddDraftedColumnToPostsTable")]
    partial class AddDraftedColumnToPostsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CategoryPost", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("PostsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "PostsId");

                    b.HasIndex("PostsId");

                    b.ToTable("CategoryPost");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<int>("PostsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("PostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("TitleLocalizationSetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TitleLocalizationSetId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Gallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TitleLocalizationSetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TitleLocalizationSetId");

                    b.ToTable("Galleries");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Localization", b =>
                {
                    b.Property<int>("LocalizationSetId")
                        .HasColumnType("int");

                    b.Property<string>("CultureCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocalizationSetId", "CultureCode");

                    b.ToTable("Localizations");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.LocalizationSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("LocalizationSets");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ContentLocalizationSetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Drafted")
                        .HasColumnType("bit");

                    b.Property<int?>("HeaderPhotoId")
                        .HasColumnType("int");

                    b.Property<int?>("MetaTitleLocalizationSetId")
                        .HasColumnType("int");

                    b.Property<int>("ReadingCount")
                        .HasColumnType("int");

                    b.Property<int>("ReadingTime")
                        .HasColumnType("int");

                    b.Property<int?>("TitleLocalizationSetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContentLocalizationSetId");

                    b.HasIndex("HeaderPhotoId");

                    b.HasIndex("MetaTitleLocalizationSetId");

                    b.HasIndex("TitleLocalizationSetId");

                    b.ToTable("Posts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Post");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.SavedFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DirectoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GalleryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GalleryId");

                    b.ToTable("SavedFiles");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("TitleLocalizationSetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TitleLocalizationSetId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.HistoryPost", b =>
                {
                    b.HasBaseType("VoiceOfKarabakh.Domain.Models.Post");

                    b.HasDiscriminator().HasValue("HistoryPost");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.NewsPost", b =>
                {
                    b.HasBaseType("VoiceOfKarabakh.Domain.Models.Post");

                    b.HasDiscriminator().HasValue("NewsPost");
                });

            modelBuilder.Entity("CategoryPost", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VoiceOfKarabakh.Domain.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VoiceOfKarabakh.Domain.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Category", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.LocalizationSet", "TitleLocalizationSet")
                        .WithMany()
                        .HasForeignKey("TitleLocalizationSetId");

                    b.Navigation("TitleLocalizationSet");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Gallery", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.LocalizationSet", "TitleLocalizationSet")
                        .WithMany()
                        .HasForeignKey("TitleLocalizationSetId");

                    b.Navigation("TitleLocalizationSet");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Localization", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.LocalizationSet", "LocalizationSet")
                        .WithMany("Localizations")
                        .HasForeignKey("LocalizationSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocalizationSet");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Post", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.LocalizationSet", "ContentLocalizationSet")
                        .WithMany()
                        .HasForeignKey("ContentLocalizationSetId");

                    b.HasOne("VoiceOfKarabakh.Domain.Models.SavedFile", "HeaderPhoto")
                        .WithMany()
                        .HasForeignKey("HeaderPhotoId");

                    b.HasOne("VoiceOfKarabakh.Domain.Models.LocalizationSet", "MetaTitleLocalizationSet")
                        .WithMany()
                        .HasForeignKey("MetaTitleLocalizationSetId");

                    b.HasOne("VoiceOfKarabakh.Domain.Models.LocalizationSet", "TitleLocalizationSet")
                        .WithMany()
                        .HasForeignKey("TitleLocalizationSetId");

                    b.Navigation("ContentLocalizationSet");

                    b.Navigation("HeaderPhoto");

                    b.Navigation("MetaTitleLocalizationSet");

                    b.Navigation("TitleLocalizationSet");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.SavedFile", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.Gallery", null)
                        .WithMany("Photos")
                        .HasForeignKey("GalleryId");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Tag", b =>
                {
                    b.HasOne("VoiceOfKarabakh.Domain.Models.LocalizationSet", "TitleLocalizationSet")
                        .WithMany()
                        .HasForeignKey("TitleLocalizationSetId");

                    b.Navigation("TitleLocalizationSet");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.Gallery", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("VoiceOfKarabakh.Domain.Models.LocalizationSet", b =>
                {
                    b.Navigation("Localizations");
                });
#pragma warning restore 612, 618
        }
    }
}
