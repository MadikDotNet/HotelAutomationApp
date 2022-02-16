using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class addMediaToRoomGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaId",
                table: "RoomGroup",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RoomGroupService",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RoomGroupId = table.Column<string>(type: "text", nullable: false),
                    ServiceId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomGroupService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomGroupService_RoomGroup_RoomGroupId",
                        column: x => x.RoomGroupId,
                        principalTable: "RoomGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomGroupService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomGroup_MediaId",
                table: "RoomGroup",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomGroupService_RoomGroupId",
                table: "RoomGroupService",
                column: "RoomGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomGroupService_ServiceId",
                table: "RoomGroupService",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroup_Media_MediaId",
                table: "RoomGroup",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroup_Media_MediaId",
                table: "RoomGroup");

            migrationBuilder.DropTable(
                name: "RoomGroupService");

            migrationBuilder.DropIndex(
                name: "IX_RoomGroup_MediaId",
                table: "RoomGroup");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "RoomGroup");
        }
    }
}
