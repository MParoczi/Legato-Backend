using Microsoft.EntityFrameworkCore.Migrations;

namespace Legato.Migrations
{
#pragma warning disable CS1591
    public partial class AddProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "ProfilePicture",
                "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "ProfilePicture",
                "AspNetUsers");
        }
    }
#pragma warning restore CS1591
}