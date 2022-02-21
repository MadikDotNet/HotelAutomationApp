using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class makeRoomGroupMediaNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroup_Media_MediaId",
                table: "RoomGroup");

            migrationBuilder.AlterColumn<string>(
                name: "MediaId",
                table: "RoomGroup",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroup_Media_MediaId",
                table: "RoomGroup",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroup_Media_MediaId",
                table: "RoomGroup");

            migrationBuilder.AlterColumn<string>(
                name: "MediaId",
                table: "RoomGroup",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroup_Media_MediaId",
                table: "RoomGroup",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
