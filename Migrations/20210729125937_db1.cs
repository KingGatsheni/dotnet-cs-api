using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_cs_api.Migrations
{
    public partial class db1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderItems_TblProducts_ProductId",
                table: "TblOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderItems_ProductId",
                table: "TblOrderItems");

            migrationBuilder.AddColumn<int>(
                name: "TblOrderItemOrderItemId",
                table: "TblProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblProducts_TblOrderItemOrderItemId",
                table: "TblProducts",
                column: "TblOrderItemOrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblProducts_TblOrderItems_TblOrderItemOrderItemId",
                table: "TblProducts",
                column: "TblOrderItemOrderItemId",
                principalTable: "TblOrderItems",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblProducts_TblOrderItems_TblOrderItemOrderItemId",
                table: "TblProducts");

            migrationBuilder.DropIndex(
                name: "IX_TblProducts_TblOrderItemOrderItemId",
                table: "TblProducts");

            migrationBuilder.DropColumn(
                name: "TblOrderItemOrderItemId",
                table: "TblProducts");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItems_ProductId",
                table: "TblOrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderItems_TblProducts_ProductId",
                table: "TblOrderItems",
                column: "ProductId",
                principalTable: "TblProducts",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
