using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class changepropv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__OrderDet__0809337CE7E3ABEC",
                table: "OrderDetail");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_orderID_ProductId",
                table: "OrderDetail",
                columns: new[] { "orderID", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_orderID_ProductId",
                table: "OrderDetail");

            migrationBuilder.CreateIndex(
                name: "UQ__OrderDet__0809337CE7E3ABEC",
                table: "OrderDetail",
                column: "orderID",
                unique: true);
        }
    }
}
