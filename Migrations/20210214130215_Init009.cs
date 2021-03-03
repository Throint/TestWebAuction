using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRazor.Migrations
{
    public partial class Init009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserCreatedId",
                table: "Items",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreatedId",
                table: "Items");
        }
    }
}
