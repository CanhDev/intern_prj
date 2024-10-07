using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class changepropv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_OrderDetail_orderDetailID",
                table: "ItemCart");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_OrderId1",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderId1",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_ItemCart_orderDetailID",
                table: "ItemCart");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "orderDetailID",
                table: "ItemCart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "OrderDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "orderDetailID",
                table: "ItemCart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId1",
                table: "OrderDetail",
                column: "OrderId1",
                unique: true,
                filter: "[OrderId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCart_orderDetailID",
                table: "ItemCart",
                column: "orderDetailID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_OrderDetail_orderDetailID",
                table: "ItemCart",
                column: "orderDetailID",
                principalTable: "OrderDetail",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_OrderId1",
                table: "OrderDetail",
                column: "OrderId1",
                principalTable: "Order",
                principalColumn: "id");
        }
    }
}
