using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class changepropv111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "soledCount",
                table: "Product",
                newName: "soldedCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "soldedCount",
                table: "Product",
                newName: "soledCount");
        }
    }
}
