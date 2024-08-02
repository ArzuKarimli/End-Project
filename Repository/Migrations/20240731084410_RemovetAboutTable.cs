using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class RemovetAboutTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutMedias");

            migrationBuilder.RenameColumn(
                name: "Vision",
                table: "Abouts",
                newName: "SectionName");

            migrationBuilder.RenameColumn(
                name: "Mission",
                table: "Abouts",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "History",
                table: "Abouts",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SectionName",
                table: "Abouts",
                newName: "Vision");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Abouts",
                newName: "Mission");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Abouts",
                newName: "History");

            migrationBuilder.CreateTable(
                name: "AboutMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AboutId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboutMedias_Abouts_AboutId",
                        column: x => x.AboutId,
                        principalTable: "Abouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AboutMedias_AboutId",
                table: "AboutMedias",
                column: "AboutId");
        }
    }
}
