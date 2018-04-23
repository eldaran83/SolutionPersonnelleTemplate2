using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Migrations
{
    public partial class addPropEtreVivant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_EtreVivants_EtreVivantID",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_EtreVivantID",
                table: "Parties");

            migrationBuilder.DropColumn(
                name: "EtreVivantID",
                table: "Parties");

            migrationBuilder.AddColumn<int>(
                name: "PartieID",
                table: "EtreVivants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EtreVivants_PartieID",
                table: "EtreVivants",
                column: "PartieID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EtreVivants_Parties_PartieID",
                table: "EtreVivants",
                column: "PartieID",
                principalTable: "Parties",
                principalColumn: "PartieID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtreVivants_Parties_PartieID",
                table: "EtreVivants");

            migrationBuilder.DropIndex(
                name: "IX_EtreVivants_PartieID",
                table: "EtreVivants");

            migrationBuilder.DropColumn(
                name: "PartieID",
                table: "EtreVivants");

            migrationBuilder.AddColumn<int>(
                name: "EtreVivantID",
                table: "Parties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parties_EtreVivantID",
                table: "Parties",
                column: "EtreVivantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_EtreVivants_EtreVivantID",
                table: "Parties",
                column: "EtreVivantID",
                principalTable: "EtreVivants",
                principalColumn: "EtreVivantID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
