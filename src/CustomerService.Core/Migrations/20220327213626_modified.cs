using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerService.Core.Migrations
{
    public partial class modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDetails_States_StateId",
                table: "CustomerDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDetails_StateId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "CustomerDetails");

            migrationBuilder.AddColumn<int>(
                name: "TBL_STATEStateId",
                table: "CustomerDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_TBL_STATEStateId",
                table: "CustomerDetails",
                column: "TBL_STATEStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDetails_States_TBL_STATEStateId",
                table: "CustomerDetails",
                column: "TBL_STATEStateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDetails_States_TBL_STATEStateId",
                table: "CustomerDetails");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDetails_TBL_STATEStateId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "TBL_STATEStateId",
                table: "CustomerDetails");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "CustomerDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_StateId",
                table: "CustomerDetails",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDetails_States_StateId",
                table: "CustomerDetails",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
