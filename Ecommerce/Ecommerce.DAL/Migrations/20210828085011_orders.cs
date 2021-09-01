using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DAL.Migrations
{
    public partial class orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    MailAddress = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    City = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    CargoPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentOption = table.Column<int>(type: "int", nullable: false),
                    RecDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastIPNO = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ProductName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductPicture = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrdersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Orders_OrdersID",
                        column: x => x.OrdersID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastDate",
                value: new DateTime(2021, 8, 28, 11, 50, 10, 551, DateTimeKind.Local).AddTicks(5592));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrdersID",
                table: "OrderDetail",
                column: "OrdersID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductID",
                table: "OrderDetail",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.UpdateData(
                table: "Admin",
                keyColumn: "ID",
                keyValue: 1,
                column: "LastDate",
                value: new DateTime(2021, 8, 15, 11, 41, 37, 15, DateTimeKind.Local).AddTicks(9031));
        }
    }
}
