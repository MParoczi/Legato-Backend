using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Legato.Migrations
{
#pragma warning disable CS1591
    public partial class AddDateOfCreationOfPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                "DateOfCreation",
                "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DateOfCreation",
                "Posts");
        }
    }
#pragma warning restore CS1591
}