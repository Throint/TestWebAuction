using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRazor.Migrations
{
    public partial class init012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Items",
                newName: "DurationTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeBegin",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeEnd",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeBegin",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DateTimeEnd",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "DurationTime",
                table: "Items",
                newName: "Time");
        }
    }
}
