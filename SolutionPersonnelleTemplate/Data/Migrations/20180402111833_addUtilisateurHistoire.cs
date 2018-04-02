using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class addUtilisateurHistoire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoireID",
                table: "Utilisateurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilisateurID",
                table: "Histoires",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "UtilisateurID",
                table: "Histoires");
        }
    }
}
