using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class addContrainteHistoire2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histoires_Utilisateurs_UtilisateurID",
                table: "Histoires");

            migrationBuilder.AlterColumn<string>(
                name: "UtilisateurID",
                table: "Histoires",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Histoires_Utilisateurs_UtilisateurID",
                table: "Histoires",
                column: "UtilisateurID",
                principalTable: "Utilisateurs",
                principalColumn: "ApplicationUserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Histoires_Utilisateurs_UtilisateurID",
                table: "Histoires",
                column: "UtilisateurID",
                principalTable: "Utilisateurs",
                principalColumn: "ApplicationUserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
