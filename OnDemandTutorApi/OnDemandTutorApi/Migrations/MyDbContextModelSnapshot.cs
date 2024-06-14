﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnDemandTutorApi.DataAccessLayer.Entity;

#nullable disable

namespace OnDemandTutorApi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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

                    b.ToTable("Role", "dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim", "dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", "dbo");
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

                    b.ToTable("UserLogin", "dbo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "dbo");
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

                    b.ToTable("UserToken", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.CoinManagement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Coin")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CoinManagement", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Level", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Star")
                        .HasColumnType("real");

                    b.Property<int>("TutorId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TutorId");

                    b.HasIndex("UserId");

                    b.ToTable("Rating", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RequestCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Request", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.RequestCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestCategory", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ResponseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RequestId")
                        .IsUnique();

                    b.ToTable("Response", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.StudentJoin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SubjectLevelId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SubjectLevelId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentJoin", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subject", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.SubjectLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Coin")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<int>("LimitMember")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TutorId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TutorId");

                    b.ToTable("SubjectLevel", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndSlot")
                        .HasColumnType("datetime2");

                    b.Property<string>("SlotName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartSlot")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubjectLevelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectLevelId");

                    b.ToTable("Time", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Tutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AcademicLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("AverageStar")
                        .HasColumnType("float");

                    b.Property<string>("CreditCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LearningMaterialDemo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OnlineStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorServiceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorServiceVideo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WorkPlace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Tutor", "dbo");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

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

                    b.ToTable("User", "dbo");
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
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", null)
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

                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.CoinManagement", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", "User")
                        .WithMany("CoinManagements")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_CoinManagement_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Rating", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.Tutor", "Tutor")
                        .WithMany("Ratings")
                        .HasForeignKey("TutorId")
                        .IsRequired()
                        .HasConstraintName("FK_Rating_Tutor");

                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Rating_User");

                    b.Navigation("Tutor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.RefreshToken", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_RefreshToken_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Request", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.RequestCategory", "Category")
                        .WithMany("Requests")
                        .HasForeignKey("RequestCategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Request_RequestCategory");

                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Request_User");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Response", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.Request", "Request")
                        .WithOne("Response")
                        .HasForeignKey("OnDemandTutorApi.DataAccessLayer.Entity.Response", "RequestId")
                        .IsRequired()
                        .HasConstraintName("FK_Response_Request");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.StudentJoin", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.SubjectLevel", "SubjectLevel")
                        .WithMany("StudentJoins")
                        .HasForeignKey("SubjectLevelId")
                        .IsRequired()
                        .HasConstraintName("FK_StudentJoin_SubjectLevel");

                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", "User")
                        .WithMany("StudentJoins")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_StudentJoin_User");

                    b.Navigation("SubjectLevel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.SubjectLevel", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.Level", "Level")
                        .WithMany("SubjectLevels")
                        .HasForeignKey("LevelId")
                        .IsRequired()
                        .HasConstraintName("FK_SubjectLevel_Level");

                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.Subject", "Subject")
                        .WithMany("SubjectLevels")
                        .HasForeignKey("SubjectId")
                        .IsRequired()
                        .HasConstraintName("FK_SubjectLevel_Subject");

                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.Tutor", "Tutor")
                        .WithMany("SubjectLevels")
                        .HasForeignKey("TutorId")
                        .IsRequired()
                        .HasConstraintName("FK_SubjectLevel_Tutor");

                    b.Navigation("Level");

                    b.Navigation("Subject");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Time", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.SubjectLevel", "SubjectLevel")
                        .WithMany("Times")
                        .HasForeignKey("SubjectLevelId")
                        .IsRequired()
                        .HasConstraintName("FK_Time_SubjectLevel");

                    b.Navigation("SubjectLevel");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Tutor", b =>
                {
                    b.HasOne("OnDemandTutorApi.DataAccessLayer.Entity.User", "User")
                        .WithOne("Tutor")
                        .HasForeignKey("OnDemandTutorApi.DataAccessLayer.Entity.Tutor", "UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Tutor_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Level", b =>
                {
                    b.Navigation("SubjectLevels");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Request", b =>
                {
                    b.Navigation("Response")
                        .IsRequired();
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.RequestCategory", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Subject", b =>
                {
                    b.Navigation("SubjectLevels");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.SubjectLevel", b =>
                {
                    b.Navigation("StudentJoins");

                    b.Navigation("Times");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.Tutor", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("SubjectLevels");
                });

            modelBuilder.Entity("OnDemandTutorApi.DataAccessLayer.Entity.User", b =>
                {
                    b.Navigation("CoinManagements");

                    b.Navigation("Ratings");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Requests");

                    b.Navigation("StudentJoins");

                    b.Navigation("Tutor")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
