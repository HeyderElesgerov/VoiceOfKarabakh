using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceOfKarabakh.Infrastructure.Migrations
{
    public partial class DeleteConstantAmoutColumnFromDonationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasConstantAmount",
                table: "Donations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasConstantAmount",
                table: "Donations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
