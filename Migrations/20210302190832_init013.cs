using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRazor.Migrations
{
    public partial class init013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastBetUserId",
                table: "Items",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastBetUserId",
                table: "Items");
        }
    }
}
