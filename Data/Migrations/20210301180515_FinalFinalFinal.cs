using Microsoft.EntityFrameworkCore.Migrations;

namespace Caser.Data.Migrations
{
    public partial class FinalFinalFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustName",
                table: "Case");

            migrationBuilder.AlterColumn<string>(
                name: "CustName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CustName",
                table: "Case",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
