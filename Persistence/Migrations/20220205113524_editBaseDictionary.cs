using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelAutomationApp.Persistence.Migrations
{
    public partial class editBaseDictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "RoomGroups");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "RoomGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RoomGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "RoomGroups",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "RoomGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "RoomGroups",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomGroups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "RoomGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "RoomGroups",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
