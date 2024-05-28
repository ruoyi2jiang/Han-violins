using Microsoft.EntityFrameworkCore.Migrations;

namespace HansViolinWebApp.Migrations
{
    public partial class alter_item_add_columns_coverUrl_and_soldInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoldInfo",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoldinfoZh",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoldInfo",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SoldinfoZh",
                table: "Items");
        }
    }
}
