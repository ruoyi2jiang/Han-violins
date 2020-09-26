using Microsoft.EntityFrameworkCore.Migrations;

namespace HansViolinWebApp.Migrations
{
    public partial class create_table_businessDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    About = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 60, nullable: true),
                    Postcode = table.Column<string>(maxLength: 10, nullable: true),
                    ImageLink = table.Column<string>(maxLength: 255, nullable: true),
                    Twitter = table.Column<string>(maxLength: 255, nullable: true),
                    Facebook = table.Column<string>(maxLength: 255, nullable: true),
                    Instagram = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessDetails");
        }
    }
}
