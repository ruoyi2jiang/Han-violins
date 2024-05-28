using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HansViolinWebApp.Migrations
{
    public partial class alter_businessDetail_add_wechat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Wechat",
                table: "BusinessDetails",
                maxLength: 255,
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wechat",
                table: "BusinessDetails");
        }
    }
}
