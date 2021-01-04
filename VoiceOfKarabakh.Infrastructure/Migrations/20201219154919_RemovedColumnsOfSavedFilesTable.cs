using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceOfKarabakh.Infrastructure.Migrations
{
    public partial class RemovedColumnsOfSavedFilesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectoryName",
                table: "SavedFiles");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "SavedFiles",
                newName: "FilePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "SavedFiles",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "DirectoryName",
                table: "SavedFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
