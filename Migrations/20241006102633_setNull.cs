using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class setNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_OrderDetail",
                table: "ItemCart");

            migrationBuilder.DropForeignKey(
                name: "FK__ItemCart__cartID__4E88ABD4",
                table: "ItemCart");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_OrderDetail",
                table: "ItemCart",
                column: "orderDetailID",
                principalTable: "OrderDetail",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK__ItemCart__cartID__4E88ABD4",
                table: "ItemCart",
                column: "cartID",
                principalTable: "Cart",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_OrderDetail",
                table: "ItemCart");

            migrationBuilder.DropForeignKey(
                name: "FK__ItemCart__cartID__4E88ABD4",
                table: "ItemCart");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_OrderDetail",
                table: "ItemCart",
                column: "orderDetailID",
                principalTable: "OrderDetail",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__ItemCart__cartID__4E88ABD4",
                table: "ItemCart",
                column: "cartID",
                principalTable: "Cart",
                principalColumn: "id");
        }
    }
}
