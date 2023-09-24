using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class namechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introdution",
                table: "AspNetUsers",
                newName: "Introduction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introduction",
                table: "AspNetUsers",
                newName: "Introdution");
        }
    }
}
