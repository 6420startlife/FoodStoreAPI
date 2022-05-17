using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStore_API.Migrations
{
    public partial class AddPhieuGiamGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "giamGia",
                table: "SanPham");

            migrationBuilder.AddColumn<string>(
                name: "maGiamGia",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PhieuGiamGia",
                columns: table => new
                {
                    maPhieuGiamGia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenPhieuGiamGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maNhap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ngayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    phanTramGiam = table.Column<int>(type: "int", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGiamGia", x => x.maPhieuGiamGia);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuGiamGia_maNhap",
                table: "PhieuGiamGia",
                column: "maNhap",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhieuGiamGia");

            migrationBuilder.DropColumn(
                name: "maGiamGia",
                table: "DonHang");

            migrationBuilder.AddColumn<int>(
                name: "giamGia",
                table: "SanPham",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
