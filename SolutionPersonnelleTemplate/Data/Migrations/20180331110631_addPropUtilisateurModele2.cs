using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class addPropUtilisateurModele2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmEmail",
                table: "Utilisateurs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Utilisateurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Utilisateurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlAvatarImage",
                table: "Utilisateurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmEmail",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "UrlAvatarImage",
                table: "Utilisateurs");
        }
    }
}
