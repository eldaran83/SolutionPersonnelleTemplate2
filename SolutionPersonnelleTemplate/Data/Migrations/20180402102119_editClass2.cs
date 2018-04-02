using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class editClass2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Histoires_HistoireID1",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Utilisateurs_UtilisateurIDApplicationUserID",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_HistoireID1",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "HistoireID1",
                table: "Parties");

            migrationBuilder.RenameColumn(
                name: "UtilisateurIDApplicationUserID",
                table: "Parties",
                newName: "UtilisateurID");

            migrationBuilder.RenameIndex(
                name: "IX_Parties_UtilisateurIDApplicationUserID",
                table: "Parties",
                newName: "IX_Parties_UtilisateurID");

            migrationBuilder.AddColumn<int>(
                name: "HistoireID",
                table: "Parties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_HistoireID",
                table: "Parties",
                column: "HistoireID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Histoires_HistoireID",
                table: "Parties",
                column: "HistoireID",
                principalTable: "Histoires",
                principalColumn: "HistoireID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Utilisateurs_UtilisateurID",
                table: "Parties",
                column: "UtilisateurID",
                principalTable: "Utilisateurs",
                principalColumn: "ApplicationUserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Histoires_HistoireID",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Utilisateurs_UtilisateurID",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_HistoireID",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "HistoireID",
                table: "Parties");

            migrationBuilder.RenameColumn(
                name: "UtilisateurID",
                table: "Parties",
                newName: "UtilisateurIDApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Parties_UtilisateurID",
                table: "Parties",
                newName: "IX_Parties_UtilisateurIDApplicationUserID");

            migrationBuilder.AddColumn<int>(
                name: "HistoireID1",
                table: "Parties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_HistoireID1",
                table: "Parties",
                column: "HistoireID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Histoires_HistoireID1",
                table: "Parties",
                column: "HistoireID1",
                principalTable: "Histoires",
                principalColumn: "HistoireID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Utilisateurs_UtilisateurIDApplicationUserID",
                table: "Parties",
                column: "UtilisateurIDApplicationUserID",
                principalTable: "Utilisateurs",
                principalColumn: "ApplicationUserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
