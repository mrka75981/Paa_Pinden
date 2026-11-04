using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaaPindenAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentKnittingPostId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Writer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KnittingPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentPictures",
                columns: table => new
                {
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPictures", x => x.Path);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnittingPostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentVideos",
                columns: table => new
                {
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentVideos", x => x.Path);
                    table.ForeignKey(
                        name: "FK_ContentVideos_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KnittingPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnittingPostContentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnittingPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnittingPosts_Contents_KnittingPostContentId",
                        column: x => x.KnittingPostContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentKnittingPostId",
                table: "Comments",
                column: "CommentKnittingPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_KnittingPostId",
                table: "Comments",
                column: "KnittingPostId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPictures_ContentId",
                table: "ContentPictures",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_KnittingPostId",
                table: "Contents",
                column: "KnittingPostId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentVideos_ContentId",
                table: "ContentVideos",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_KnittingPosts_KnittingPostContentId",
                table: "KnittingPosts",
                column: "KnittingPostContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_KnittingPosts_CommentKnittingPostId",
                table: "Comments",
                column: "CommentKnittingPostId",
                principalTable: "KnittingPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_KnittingPosts_KnittingPostId",
                table: "Comments",
                column: "KnittingPostId",
                principalTable: "KnittingPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContentPictures_Contents_ContentId",
                table: "ContentPictures",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_KnittingPosts_KnittingPostId",
                table: "Contents",
                column: "KnittingPostId",
                principalTable: "KnittingPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_KnittingPosts_KnittingPostId",
                table: "Contents");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ContentPictures");

            migrationBuilder.DropTable(
                name: "ContentVideos");

            migrationBuilder.DropTable(
                name: "KnittingPosts");

            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
