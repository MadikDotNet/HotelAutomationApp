using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class editRoomsMedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroups_RoomGroups_ParentId",
                table: "RoomGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomGroups_RoomGroupId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomGroups",
                table: "RoomGroups");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "RoomGroups",
                newName: "RoomGroup");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_RoomGroupId",
                table: "Room",
                newName: "IX_Room_RoomGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomGroups_ParentId",
                table: "RoomGroup",
                newName: "IX_RoomGroup_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomGroup",
                table: "RoomGroup",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomMedia",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RoomId = table.Column<string>(type: "text", nullable: false),
                    MediaId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomMedia_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomMedia_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomMedia_MediaId",
                table: "RoomMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMedia_RoomId",
                table: "RoomMedia",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomGroup_RoomGroupId",
                table: "Room",
                column: "RoomGroupId",
                principalTable: "RoomGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroup_RoomGroup_ParentId",
                table: "RoomGroup",
                column: "ParentId",
                principalTable: "RoomGroup",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomGroup_RoomGroupId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroup_RoomGroup_ParentId",
                table: "RoomGroup");

            migrationBuilder.DropTable(
                name: "RoomMedia");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomGroup",
                table: "RoomGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.RenameTable(
                name: "RoomGroup",
                newName: "RoomGroups");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameIndex(
                name: "IX_RoomGroup_ParentId",
                table: "RoomGroups",
                newName: "IX_RoomGroups_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_RoomGroupId",
                table: "Rooms",
                newName: "IX_Rooms_RoomGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomGroups",
                table: "RoomGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RoomImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    RoomId = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomImages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomImages_RoomId",
                table: "RoomImages",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroups_RoomGroups_ParentId",
                table: "RoomGroups",
                column: "ParentId",
                principalTable: "RoomGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomGroups_RoomGroupId",
                table: "Rooms",
                column: "RoomGroupId",
                principalTable: "RoomGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
