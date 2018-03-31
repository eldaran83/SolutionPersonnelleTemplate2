using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class AddUtilisateurClassModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "Utilisateurs",
                 columns: table => new
                 {
                     ApplicationUserID = table.Column<string>(nullable: false),
                     DateCreationUtilisateur = table.Column<DateTime>(nullable: false),
                     DateDeNaissance = table.Column<DateTime>(nullable: false),
                     ProfilUtilisateurComplet = table.Column<bool>(nullable: false),
                     Pseudo = table.Column<string>(nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_Utilisateurs", x => x.ApplicationUserID);
                     table.ForeignKey(
                         name: "FK_Utilisateurs_AspNetUsers_ApplicationUserID",
                         column: x => x.ApplicationUserID,
                         principalTable: "AspNetUsers",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
