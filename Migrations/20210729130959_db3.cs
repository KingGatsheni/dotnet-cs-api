using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_cs_api.Migrations
{
    public partial class db3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderItems_TblOrders_TblOrderOrderId",
                table: "TblOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TblProducts_TblOrderItems_TblOrderItemOrderItemId",
                table: "TblProducts");

            migrationBuilder.DropIndex(
                name: "IX_TblProducts_TblOrderItemOrderItemId",
                table: "TblProducts");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderItems_TblOrderOrderId",
                table: "TblOrderItems");

            migrationBuilder.DropColumn(
                name: "TblOrderItemOrderItemId",
                table: "TblProducts");

            migrationBuilder.DropColumn(
                name: "TblOrderOrderId",
                table: "TblOrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItems_OrderId",
                table: "TblOrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItems_ProductId",
                table: "TblOrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderItems_TblOrders_OrderId",
                table: "TblOrderItems",
                column: "OrderId",
                principalTable: "TblOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderItems_TblProducts_ProductId",
                table: "TblOrderItems",
                column: "ProductId",
                principalTable: "TblProducts",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderItems_TblOrders_OrderId",
                table: "TblOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderItems_TblProducts_ProductId",
                table: "TblOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderItems_OrderId",
                table: "TblOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderItems_ProductId",
                table: "TblOrderItems");

            migrationBuilder.AddColumn<int>(
                name: "TblOrderItemOrderItemId",
                table: "TblProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TblOrderOrderId",
                table: "TblOrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblProducts_TblOrderItemOrderItemId",
                table: "TblProducts",
                column: "TblOrderItemOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItems_TblOrderOrderId",
                table: "TblOrderItems",
                column: "TblOrderOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderItems_TblOrders_TblOrderOrderId",
                table: "TblOrderItems",
                column: "TblOrderOrderId",
                principalTable: "TblOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TblProducts_TblOrderItems_TblOrderItemOrderItemId",
                table: "TblProducts",
                column: "TblOrderItemOrderItemId",
                principalTable: "TblOrderItems",
                principalColumn: "OrderItemId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
