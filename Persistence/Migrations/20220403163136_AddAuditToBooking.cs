using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class AddAuditToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Booking",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Booking",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Booking",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Booking",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Booking");
        }
    }
}
