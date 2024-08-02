using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class RemoveTitleDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Content",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Abouts");

            migrationBuilder.RenameTable(
                name: "Content",
                newName: "Services");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Content");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Content",
                table: "Content",
                column: "Id");
        }
    }
}
