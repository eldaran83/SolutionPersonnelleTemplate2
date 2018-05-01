using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Migrations
{
    public partial class addLogicPerso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttaqueMaitriseArme",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttaqueMaitriseMagique",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusAuDegatMagique",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusAuDegatPhysique",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusCharisme",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusConstitution",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusDexterite",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusForce",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusIntelligence",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusSagesse",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClasseArmure",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClasseDuPersonnage",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NiveauDuPersonnage",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointsExperience",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Reflexe",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sagesse",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SexePersonnage",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vigueur",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Volonte",
                table: "Personnes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BonusDesClassesJoueurs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bonus = table.Column<int>(nullable: false),
                    Classe = table.Column<int>(nullable: false),
                    Niveau = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusDesClassesJoueurs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusDesClassesJoueurs");

            migrationBuilder.DropColumn(
                name: "AttaqueMaitriseArme",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "AttaqueMaitriseMagique",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusAuDegatMagique",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusAuDegatPhysique",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusCharisme",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusConstitution",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusDexterite",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusForce",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusIntelligence",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "BonusSagesse",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "ClasseArmure",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "ClasseDuPersonnage",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "NiveauDuPersonnage",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "PointsExperience",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Reflexe",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Sagesse",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "SexePersonnage",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Vigueur",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Volonte",
                table: "Personnes");
        }
    }
}
