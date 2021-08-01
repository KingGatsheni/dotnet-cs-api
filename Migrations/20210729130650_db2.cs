using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_cs_api.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderItems_TblOrders_OrderId",
                table: "TblOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderItems_OrderId",
                table: "TblOrderItems");

            migrationBuilder.AddColumn<int>(
                name: "TblOrderOrderId",
                table: "TblOrderItems",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderItems_TblOrders_TblOrderOrderId",
                table: "TblOrderItems");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderItems_TblOrderOrderId",
                table: "TblOrderItems");

            migrationBuilder.DropColumn(
                name: "TblOrderOrderId",
                table: "TblOrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItems_OrderId",
                table: "TblOrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderItems_TblOrders_OrderId",
                table: "TblOrderItems",
                column: "OrderId",
                principalTable: "TblOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
