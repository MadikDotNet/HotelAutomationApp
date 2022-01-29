using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "RoomGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "RoomGroups",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomGroups_ParentId",
                table: "RoomGroups",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroups_RoomGroups_ParentId",
                table: "RoomGroups",
                column: "ParentId",
                principalTable: "RoomGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroups_RoomGroups_ParentId",
                table: "RoomGroups");

            migrationBuilder.DropIndex(
                name: "IX_RoomGroups_ParentId",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "RoomGroups");
        }
    }
}
