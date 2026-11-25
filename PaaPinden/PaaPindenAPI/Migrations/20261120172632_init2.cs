using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaaPindenAPI.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Needle", "Note", "PicturePath", "Recipe", "Round", "Stitch", "Title", "UserId", "YarnType" },
                values: new object[] { 1, "2mm", null, null, "big text", 0, 0, "Min nye halsedisse", "84cd148c-e055-4d01-8a32-d06e7489e2a3", "Rødt garn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
