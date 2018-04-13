using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Migrations
{
    public partial class ADDPropMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomAction1",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomAction2",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomAction3",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomAction1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NomAction2",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NomAction3",
                table: "Messages");
        }
    }
}
