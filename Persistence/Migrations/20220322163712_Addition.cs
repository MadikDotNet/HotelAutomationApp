using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class Addition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroup_FileMetadata_MediaId",
                table: "RoomGroup");

            migrationBuilder.RenameColumn(
                name: "MediaId",
                table: "RoomGroup",
                newName: "FileMetadataId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomGroup_MediaId",
                table: "RoomGroup",
                newName: "IX_RoomGroup_FileMetadataId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroup_FileMetadata_FileMetadataId",
                table: "RoomGroup",
                column: "FileMetadataId",
                principalTable: "FileMetadata",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroup_FileMetadata_FileMetadataId",
                table: "RoomGroup");

            migrationBuilder.RenameColumn(
                name: "FileMetadataId",
                table: "RoomGroup",
                newName: "MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomGroup_FileMetadataId",
                table: "RoomGroup",
                newName: "IX_RoomGroup_MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroup_FileMetadata_MediaId",
                table: "RoomGroup",
                column: "MediaId",
                principalTable: "FileMetadata",
                principalColumn: "Id");
        }
    }
}
