using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SolutionPersonnelleTemplate.Data.Migrations
{
    public partial class changestroryprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resume",
                table: "Histoires",
                newName: "Synopsis");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Synopsis",
                table: "Histoires",
                newName: "Resume");
        }
    }
}
