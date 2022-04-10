using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class ServiceGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdditional",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Service");

            migrationBuilder.AddColumn<string>(
                name: "ServiceGroupId",
                table: "Service",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ServiceGroup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.AddColumn<bool>(
                name: "IsAdditional",
                table: "Service",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Service",
                type: "text",
                nullable: true);
        }
    }
}
