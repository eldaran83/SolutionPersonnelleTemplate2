using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class changeHistoire2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilisateurs_Histoires_HistoireID",
                table: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_HistoireID",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "HistoireID",
                table: "Utilisateurs");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Histoires",
                newName: "UtilisateurID");

            migrationBuilder.AlterColumn<string>(
                name: "UtilisateurID",
                table: "Histoires",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Histoires_UtilisateurID",
                table: "Histoires",
                column: "UtilisateurID");

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

            migrationBuilder.DropIndex(
                name: "IX_Histoires_UtilisateurID",
                table: "Histoires");

            migrationBuilder.RenameColumn(
                name: "UtilisateurID",
                table: "Histoires",
                newName: "ApplicationUserID");

            migrationBuilder.AddColumn<int>(
                name: "HistoireID",
                table: "Utilisateurs",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserID",
                table: "Histoires",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_HistoireID",
                table: "Utilisateurs",
                column: "HistoireID");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilisateurs_Histoires_HistoireID",
                table: "Utilisateurs",
                column: "HistoireID",
                principalTable: "Histoires",
                principalColumn: "HistoireID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
