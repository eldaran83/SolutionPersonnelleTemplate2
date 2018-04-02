using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class editClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Histoires_HistoireID2",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Utilisateurs_UtilisateurApplicationUserID",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_HistoireID2",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_UtilisateurApplicationUserID",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "HistoireID2",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "UtilisateurApplicationUserID",
                table: "Parties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoireID2",
                table: "Parties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilisateurApplicationUserID",
                table: "Parties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_HistoireID2",
                table: "Parties",
                column: "HistoireID2");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_UtilisateurApplicationUserID",
                table: "Parties",
                column: "UtilisateurApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Histoires_HistoireID2",
                table: "Parties",
                column: "HistoireID2",
                principalTable: "Histoires",
                principalColumn: "HistoireID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Utilisateurs_UtilisateurApplicationUserID",
                table: "Parties",
                column: "UtilisateurApplicationUserID",
                principalTable: "Utilisateurs",
                principalColumn: "ApplicationUserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
