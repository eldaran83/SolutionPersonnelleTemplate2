using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class AddLienMessageEnfants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MessageID",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            migrationBuilder.AddColumn<int>(
                name: "NumeroMessageParent",
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

            migrationBuilder.DropColumn(
                name: "NumeroMessageParent",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "MessageID",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
