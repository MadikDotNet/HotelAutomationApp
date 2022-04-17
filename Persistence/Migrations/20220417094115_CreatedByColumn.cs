using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class CreatedByColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Appeal",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appeal_CreatedBy",
                table: "Appeal",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeal_AspNetUsers_CreatedBy",
                table: "Appeal",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appeal_AspNetUsers_CreatedBy",
                table: "Appeal");

            migrationBuilder.DropIndex(
                name: "IX_Appeal_CreatedBy",
                table: "Appeal");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Appeal");
        }
    }
}
