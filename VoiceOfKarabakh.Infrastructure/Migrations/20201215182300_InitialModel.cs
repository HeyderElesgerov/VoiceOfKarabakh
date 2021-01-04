using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceOfKarabakh.Infrastructure.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalizationSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleLocalizationSetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_LocalizationSets_TitleLocalizationSetId",
                        column: x => x.TitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleLocalizationSetId = table.Column<int>(type: "int", nullable: true),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galleries_LocalizationSets_TitleLocalizationSetId",
                        column: x => x.TitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    CultureCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocalizationSetId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => new { x.LocalizationSetId, x.CultureCode });
                    table.ForeignKey(
                        name: "FK_Localizations_LocalizationSets_LocalizationSetId",
                        column: x => x.LocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleLocalizationSetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_LocalizationSets_TitleLocalizationSetId",
                        column: x => x.TitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SavedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GalleryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedFiles_Galleries_GalleryId",
                        column: x => x.GalleryId,
                        principalTable: "Galleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleLocalizationSetId = table.Column<int>(type: "int", nullable: true),
                    MetaTitleLocalizationSetId = table.Column<int>(type: "int", nullable: true),
                    ContentLocalizationSetId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadingCount = table.Column<int>(type: "int", nullable: false),
                    HeaderPhotoId = table.Column<int>(type: "int", nullable: true),
                    ReadingTime = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_LocalizationSets_ContentLocalizationSetId",
                        column: x => x.ContentLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_LocalizationSets_MetaTitleLocalizationSetId",
                        column: x => x.MetaTitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_LocalizationSets_TitleLocalizationSetId",
                        column: x => x.TitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_SavedFiles_HeaderPhotoId",
                        column: x => x.HeaderPhotoId,
                        principalTable: "SavedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPost",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPost", x => new { x.CategoriesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_CategoryPost_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPost_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => new { x.PostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TitleLocalizationSetId",
                table: "Categories",
                column: "TitleLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPost_PostsId",
                table: "CategoryPost",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_TitleLocalizationSetId",
                table: "Galleries",
                column: "TitleLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ContentLocalizationSetId",
                table: "Posts",
                column: "ContentLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_HeaderPhotoId",
                table: "Posts",
                column: "HeaderPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_MetaTitleLocalizationSetId",
                table: "Posts",
                column: "MetaTitleLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_TitleLocalizationSetId",
                table: "Posts",
                column: "TitleLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagsId",
                table: "PostTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedFiles_GalleryId",
                table: "SavedFiles",
                column: "GalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TitleLocalizationSetId",
                table: "Tags",
                column: "TitleLocalizationSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPost");

            migrationBuilder.DropTable(
                name: "Localizations");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "SavedFiles");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "LocalizationSets");
        }
    }
}
