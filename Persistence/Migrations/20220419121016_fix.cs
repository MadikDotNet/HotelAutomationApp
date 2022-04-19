using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceGroup_ServiceGroupId",
                table: "Service");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceGroupId",
                table: "Service",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceGroup_ServiceGroupId",
                table: "Service",
                column: "ServiceGroupId",
                principalTable: "ServiceGroup",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceGroup_ServiceGroupId",
                table: "Service");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceGroupId",
                table: "Service",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceGroup_ServiceGroupId",
                table: "Service",
                column: "ServiceGroupId",
                principalTable: "ServiceGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
