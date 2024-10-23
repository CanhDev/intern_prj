using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class updateRemoveUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_user",
                table: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_user",
                table: "Order",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_user",
                table: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_user",
                table: "Order",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
