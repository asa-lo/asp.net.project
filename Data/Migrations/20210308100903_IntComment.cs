using Microsoft.EntityFrameworkCore.Migrations;

namespace Caser.Data.Migrations
{
    public partial class IntComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaseIntComment",
                table: "Case",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseIntComment",
                table: "Case");
        }
    }
}
