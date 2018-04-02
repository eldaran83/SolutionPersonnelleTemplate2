using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class AddClassBO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Histoires",
                columns: table => new
                {
                    HistoireID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreDeFoisJouee = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    Titre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histoires", x => x.HistoireID);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartieID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HistoireID1 = table.Column<int>(nullable: true),
                    HistoireID2 = table.Column<int>(nullable: true),
                    Horodatage = table.Column<TimeSpan>(nullable: false),
                    UtilisateurApplicationUserID = table.Column<string>(nullable: true),
                    UtilisateurIDApplicationUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartieID);
                    table.ForeignKey(
                        name: "FK_Parties_Histoires_HistoireID1",
                        column: x => x.HistoireID1,
                        principalTable: "Histoires",
                        principalColumn: "HistoireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parties_Histoires_HistoireID2",
                        column: x => x.HistoireID2,
                        principalTable: "Histoires",
                        principalColumn: "HistoireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parties_Utilisateurs_UtilisateurApplicationUserID",
                        column: x => x.UtilisateurApplicationUserID,
                        principalTable: "Utilisateurs",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parties_Utilisateurs_UtilisateurIDApplicationUserID",
                        column: x => x.UtilisateurIDApplicationUserID,
                        principalTable: "Utilisateurs",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parties_HistoireID1",
                table: "Parties",
                column: "HistoireID1");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_HistoireID2",
                table: "Parties",
                column: "HistoireID2");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_UtilisateurApplicationUserID",
                table: "Parties",
                column: "UtilisateurApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_UtilisateurIDApplicationUserID",
                table: "Parties",
                column: "UtilisateurIDApplicationUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Histoires");
        }
    }
}
