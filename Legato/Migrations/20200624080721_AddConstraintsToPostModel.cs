using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Legato.Migrations
{
#pragma warning disable CS1591
    public partial class AddConstraintsToPostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "Title",
                "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Content",
                "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                "DateOfEdit",
                "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "DateOfEdit",
                "Posts");

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Posts",
                "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Content",
                "Posts",
                "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
#pragma warning restore CS1591
}