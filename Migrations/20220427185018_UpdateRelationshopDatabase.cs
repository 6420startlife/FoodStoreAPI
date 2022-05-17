using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStore_API.Migrations
{
    public partial class UpdateRelationshopDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "trangThai",
                table: "SanPham");

            migrationBuilder.AddColumn<int>(
                name: "trangThaiSanPham",
                table: "SanPham",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "trangThaiDonHang",
                table: "DonHang",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "nguoiNhan",
                table: "DonHang",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "trangThaiSanPham",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "nguoiNhan",
                table: "DonHang");

            migrationBuilder.AddColumn<int>(
                name: "trangThai",
                table: "SanPham",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "trangThaiDonHang",
                table: "DonHang",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
