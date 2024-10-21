using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class addStatusProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Product",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Product");
        }
    }
}
