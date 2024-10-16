using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class cacasder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cart__userID__4BAC3F29",
                table: "Cart");

            migrationBuilder.AddForeignKey(
                name: "FK__Cart__userID__4BAC3F29",
                table: "Cart",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cart__userID__4BAC3F29",
                table: "Cart");

            migrationBuilder.AddForeignKey(
                name: "FK__Cart__userID__4BAC3F29",
                table: "Cart",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
