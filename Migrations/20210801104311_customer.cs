using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_cs_api.Migrations
{
    public partial class customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "TblOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TblCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Residence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCustomers", x => x.CustomerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblOrders_CustomerId",
                table: "TblOrders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrders_TblCustomers_CustomerId",
                table: "TblOrders",
                column: "CustomerId",
                principalTable: "TblCustomers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrders_TblCustomers_CustomerId",
                table: "TblOrders");

            migrationBuilder.DropTable(
                name: "TblCustomers");

            migrationBuilder.DropIndex(
                name: "IX_TblOrders_CustomerId",
                table: "TblOrders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "TblOrders");
        }
    }
}
