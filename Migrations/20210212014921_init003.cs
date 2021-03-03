using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRazor.Migrations
{
    public partial class init003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "EmailAddress");

            migrationBuilder.AddColumn<decimal>(
                name: "BeginPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RedemtionPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RedemtionPrice",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Users",
                newName: "Email");
        }
    }
}
