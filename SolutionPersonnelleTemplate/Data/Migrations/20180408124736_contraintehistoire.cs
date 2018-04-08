using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class contraintehistoire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histoires_Utilisateurs_UtilisateurID",
                table: "Histoires");

            migrationBuilder.AlterColumn<string>(
                name: "UtilisateurID",
                table: "Histoires",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Histoires",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resume",
                table: "Histoires",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 600,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Histoires_Utilisateurs_UtilisateurID",
                table: "Histoires",
                column: "UtilisateurID",
                principalTable: "Utilisateurs",
                principalColumn: "ApplicationUserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histoires_Utilisateurs_UtilisateurID",
                table: "Histoires");

            migrationBuilder.AlterColumn<string>(
                name: "UtilisateurID",
                table: "Histoires",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Titre",
                table: "Histoires",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Resume",
                table: "Histoires",
                maxLength: 600,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 600);

            migrationBuilder.AddForeignKey(
                name: "FK_Histoires_Utilisateurs_UtilisateurID",
                table: "Histoires",
                column: "UtilisateurID",
                principalTable: "Utilisateurs",
                principalColumn: "ApplicationUserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
