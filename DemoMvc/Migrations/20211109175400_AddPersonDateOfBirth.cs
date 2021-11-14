using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoMvc.Migrations
{
    public partial class AddPersonDateOfBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // We want to require DoB, so let's delete everyone who doesn't have one
            migrationBuilder.Sql("DELETE FROM Persons");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Persons",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Persons");
        }
    }
}
