using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class changeMessageProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageEnfant1",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MessageEnfant2",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MessageEnfant3",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroMessageEnfant1",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroMessageEnfant2",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroMessageEnfant3",
                table: "Messages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageEnfant1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageEnfant2",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageEnfant3",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NumeroMessageEnfant1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NumeroMessageEnfant2",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "NumeroMessageEnfant3",
                table: "Messages");
        }
    }
}
