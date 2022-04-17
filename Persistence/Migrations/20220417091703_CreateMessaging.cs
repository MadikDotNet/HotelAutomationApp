using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class CreateMessaging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appeal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FeedbackId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appeal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AppealId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback_Appeal_AppealId",
                        column: x => x.AppealId,
                        principalTable: "Appeal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appeal_FeedbackId",
                table: "Appeal",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_AppealId",
                table: "Feedback",
                column: "AppealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeal_Feedback_FeedbackId",
                table: "Appeal",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appeal_Feedback_FeedbackId",
                table: "Appeal");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Appeal");
        }
    }
}
