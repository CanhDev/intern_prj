using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class vvvv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__ItemCart__2D10D14BDBBF2276",
                table: "ItemCart");

            migrationBuilder.CreateIndex(
                name: "UQ__ItemCart__2D10D14BDBBF2276",
                table: "ItemCart",
                column: "productID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__ItemCart__2D10D14BDBBF2276",
                table: "ItemCart");

            migrationBuilder.CreateIndex(
                name: "UQ__ItemCart__2D10D14BDBBF2276",
                table: "ItemCart",
                column: "productID",
                unique: true,
                filter: "[productID] IS NOT NULL");
        }
    }
}
