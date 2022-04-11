using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class RefactorService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Service",
                newName: "PricePerHour");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Room",
                newName: "PricePerHour");

            migrationBuilder.AddColumn<string>(
                name: "ServiceGroupId",
                table: "Service",
                type: "text",
                nullable: true,
                defaultValue: (string)null);

            migrationBuilder.CreateTable(
                name: "ServiceGroup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceGroupId",
                table: "Service",
                column: "ServiceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceGroup_ServiceGroupId",
                table: "Service",
                column: "ServiceGroupId",
                principalTable: "ServiceGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceGroup_ServiceGroupId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "ServiceGroup");

            migrationBuilder.DropIndex(
                name: "IX_Service_ServiceGroupId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceGroupId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "Service",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PricePerHour",
                table: "Room",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Service",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Room",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
