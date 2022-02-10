using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class addService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGroup_RoomGroup_ParentId",
                table: "RoomGroup");

            migrationBuilder.DropIndex(
                name: "IX_RoomGroup_ParentId",
                table: "RoomGroup");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "RoomGroup");

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "RoomGroup",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomGroup_ParentId",
                table: "RoomGroup",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGroup_RoomGroup_ParentId",
                table: "RoomGroup",
                column: "ParentId",
                principalTable: "RoomGroup",
                principalColumn: "Id");
        }
    }
}
