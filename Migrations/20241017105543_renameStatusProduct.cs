using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class renameStatusProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Product",
                newName: "OutOfStockstatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutOfStockstatus",
                table: "Product",
                newName: "status");
        }
    }
}
