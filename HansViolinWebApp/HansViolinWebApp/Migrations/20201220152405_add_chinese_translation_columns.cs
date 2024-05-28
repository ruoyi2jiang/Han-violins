using Microsoft.EntityFrameworkCore.Migrations;

namespace HansViolinWebApp.Migrations
{
    public partial class add_chinese_translation_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentZh",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleZh",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionZh",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemNameZh",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginInfoZh",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryNameZh",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionZh",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutZh",
                table: "BusinessDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentZh",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TitleZh",
                table: "News");

            migrationBuilder.DropColumn(
                name: "DescriptionZh",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemNameZh",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OriginInfoZh",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryNameZh",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DescriptionZh",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AboutZh",
                table: "BusinessDetails");
        }
    }
}
