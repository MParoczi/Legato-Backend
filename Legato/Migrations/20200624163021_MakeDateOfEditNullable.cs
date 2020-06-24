using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Legato.Migrations
{
#pragma warning disable CS1591
    public partial class MakeDateOfEditNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Edited",
                "Posts");

            migrationBuilder.AddColumn<DateTime>(
                "DateOfEdit",
                "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DateOfEdit",
                "Posts");

            migrationBuilder.AddColumn<DateTime>(
                "Edited",
                "Posts",
                "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
#pragma warning restore CS1591
}