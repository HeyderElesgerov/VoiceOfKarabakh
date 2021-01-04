using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceOfKarabakh.Infrastructure.Migrations
{
    public partial class AddDraftedColumnToPostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Drafted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Drafted",
                table: "Posts");
        }
    }
}
