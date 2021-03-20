using Microsoft.EntityFrameworkCore.Migrations;

namespace Caser.Data.Migrations
{
    public partial class Final1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_Customer_CustomersCustId",
                table: "Case");

            migrationBuilder.AlterColumn<int>(
                name: "CustomersCustId",
                table: "Case",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Case_Customer_CustomersCustId",
                table: "Case",
                column: "CustomersCustId",
                principalTable: "Customer",
                principalColumn: "CustId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_Customer_CustomersCustId",
                table: "Case");

            migrationBuilder.AlterColumn<int>(
                name: "CustomersCustId",
                table: "Case",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Case_Customer_CustomersCustId",
                table: "Case",
                column: "CustomersCustId",
                principalTable: "Customer",
                principalColumn: "CustId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
