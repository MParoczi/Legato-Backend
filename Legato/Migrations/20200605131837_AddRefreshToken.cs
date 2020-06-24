using Microsoft.EntityFrameworkCore.Migrations;

namespace Legato.Migrations
{
#pragma warning disable CS1591
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "RefreshToken",
                "AspNetUsers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "RefreshToken",
                "AspNetUsers");
        }
    }
#pragma warning restore CS1591
}