using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace intern_prj.Migrations
{
    /// <inheritdoc />
    public partial class addpropv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "statusPayment",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "Đang chờ");

            migrationBuilder.AddColumn<string>(
                name: "statusShipping",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "Chưa thanh toán");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statusPayment",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "statusShipping",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "pending");
        }
    }
}
