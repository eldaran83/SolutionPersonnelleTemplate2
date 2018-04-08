﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SolutionPersonnelleTemplate.Data;
using System;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180408130114_change")]
    partial class change
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.ApplicationUser", b =>
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

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Histoire", b =>
                {
                    b.Property<int>("HistoireID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Createur");

                    b.Property<int>("NombreDeFoisJouee");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasMaxLength(600);

                    b.Property<int>("Score");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("UrlMedia");

                    b.Property<string>("UtilisateurID");

                    b.HasKey("HistoireID");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Histoires");
                });

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contenu");

                    b.Property<int>("HistoireID");

                    b.Property<string>("Titre");

                    b.HasKey("MessageID");

                    b.HasIndex("HistoireID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Partie", b =>
                {
                    b.Property<int>("PartieID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HistoireID");

                    b.Property<TimeSpan>("Horodatage");

                    b.Property<string>("UtilisateurID");

                    b.HasKey("PartieID");

                    b.HasIndex("HistoireID");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Utilisateur", b =>
                {
                    b.Property<string>("ApplicationUserID");

                    b.Property<bool>("ConfirmEmail");

                    b.Property<DateTime>("DateCreationUtilisateur");

                    b.Property<DateTime>("DateDeNaissance");

                    b.Property<string>("Email");

                    b.Property<bool>("ProfilUtilisateurComplet");

                    b.Property<string>("Pseudo");

                    b.Property<string>("Role");

                    b.Property<string>("UrlAvatarImage");

                    b.HasKey("ApplicationUserID");

                    b.ToTable("Utilisateurs");
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
                    b.HasOne("SolutionPersonnelleTemplate.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SolutionPersonnelleTemplate.Models.ApplicationUser")
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

                    b.HasOne("SolutionPersonnelleTemplate.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SolutionPersonnelleTemplate.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Histoire", b =>
                {
                    b.HasOne("SolutionPersonnelleTemplate.Models.BO.Utilisateur", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurID");
                });

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Message", b =>
                {
                    b.HasOne("SolutionPersonnelleTemplate.Models.BO.Histoire", "Histoire")
                        .WithMany("Messages")
                        .HasForeignKey("HistoireID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Partie", b =>
                {
                    b.HasOne("SolutionPersonnelleTemplate.Models.BO.Histoire", "Histoire")
                        .WithMany()
                        .HasForeignKey("HistoireID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SolutionPersonnelleTemplate.Models.BO.Utilisateur", "Utilisateur")
                        .WithMany("Parties")
                        .HasForeignKey("UtilisateurID");
                });

            modelBuilder.Entity("SolutionPersonnelleTemplate.Models.BO.Utilisateur", b =>
                {
                    b.HasOne("SolutionPersonnelleTemplate.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("Utilisateur")
                        .HasForeignKey("SolutionPersonnelleTemplate.Models.BO.Utilisateur", "ApplicationUserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
