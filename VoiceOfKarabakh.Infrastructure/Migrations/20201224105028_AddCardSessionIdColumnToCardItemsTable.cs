using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceOfKarabakh.Infrastructure.Migrations
{
    public partial class AddCardSessionIdColumnToCardItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "CardItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_CardId",
                table: "CardItems",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardItems_Cards_CardId",
                table: "CardItems",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardItems_Cards_CardId",
                table: "CardItems");

            migrationBuilder.DropIndex(
                name: "IX_CardItems_CardId",
                table: "CardItems");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CardItems");
        }
    }
}
