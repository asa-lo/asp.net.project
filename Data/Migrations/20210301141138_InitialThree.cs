using Microsoft.EntityFrameworkCore.Migrations;

namespace Caser.Data.Migrations
{
    public partial class InitialThree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Case_Customer_CustomersCustId",
                table: "Case");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Case",
                table: "Case");

            migrationBuilder.RenameTable(
                name: "Case",
                newName: "Cases");

            migrationBuilder.RenameIndex(
                name: "IX_Case_CustomersCustId",
                table: "Cases",
                newName: "IX_Cases_CustomersCustId");

            migrationBuilder.AlterColumn<string>(
                name: "CustName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cases",
                table: "Cases",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Customer_CustomersCustId",
                table: "Cases",
                column: "CustomersCustId",
                principalTable: "Customer",
                principalColumn: "CustId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Customer_CustomersCustId",
                table: "Cases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cases",
                table: "Cases");

            migrationBuilder.RenameTable(
                name: "Cases",
                newName: "Case");

            migrationBuilder.RenameIndex(
                name: "IX_Cases_CustomersCustId",
                table: "Case",
                newName: "IX_Case_CustomersCustId");

            migrationBuilder.AlterColumn<string>(
                name: "CustName",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Case",
                table: "Case",
                column: "CaseId");

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
