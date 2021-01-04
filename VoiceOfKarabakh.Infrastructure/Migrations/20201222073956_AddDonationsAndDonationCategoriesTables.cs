using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceOfKarabakh.Infrastructure.Migrations
{
    public partial class AddDonationsAndDonationCategoriesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleLocalizationSetId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonationCategories_LocalizationSets_TitleLocalizationSetId",
                        column: x => x.TitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationCategoryId = table.Column<int>(type: "int", nullable: false),
                    TitleLocalizationSetId = table.Column<int>(type: "int", nullable: true),
                    MetaTitleLocalizationSetId = table.Column<int>(type: "int", nullable: true),
                    ContentLocalizationSetId = table.Column<int>(type: "int", nullable: true),
                    HeaderPhotoId = table.Column<int>(type: "int", nullable: true),
                    HasConstantAmount = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_DonationCategories_DonationCategoryId",
                        column: x => x.DonationCategoryId,
                        principalTable: "DonationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donations_LocalizationSets_ContentLocalizationSetId",
                        column: x => x.ContentLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_LocalizationSets_MetaTitleLocalizationSetId",
                        column: x => x.MetaTitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_LocalizationSets_TitleLocalizationSetId",
                        column: x => x.TitleLocalizationSetId,
                        principalTable: "LocalizationSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Donations_SavedFiles_HeaderPhotoId",
                        column: x => x.HeaderPhotoId,
                        principalTable: "SavedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationCategories_TitleLocalizationSetId",
                table: "DonationCategories",
                column: "TitleLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_ContentLocalizationSetId",
                table: "Donations",
                column: "ContentLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonationCategoryId",
                table: "Donations",
                column: "DonationCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_HeaderPhotoId",
                table: "Donations",
                column: "HeaderPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_MetaTitleLocalizationSetId",
                table: "Donations",
                column: "MetaTitleLocalizationSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_TitleLocalizationSetId",
                table: "Donations",
                column: "TitleLocalizationSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "DonationCategories");
        }
    }
}
