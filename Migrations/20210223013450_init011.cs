using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRazor.Migrations
{
    public partial class init011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BetWasDone",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BetWasDone",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");
        }
    }
}
