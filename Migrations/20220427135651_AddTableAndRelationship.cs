using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStore_API.Migrations
{
    public partial class AddTableAndRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    maDanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.maDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    maTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenTaiKhoan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    matKhau = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false),
                    hoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quyenTaiKhoan = table.Column<int>(type: "int", nullable: false),
                    anhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "date", nullable: true),
                    soDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.maTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    maSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    giaSanPham = table.Column<double>(type: "float", nullable: false),
                    trangThai = table.Column<int>(type: "int", nullable: false),
                    soLuongTonKho = table.Column<int>(type: "int", nullable: false),
                    anhSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    giamGia = table.Column<int>(type: "int", nullable: false),
                    maDanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.maSanPham);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc",
                        column: x => x.maDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "maDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    maDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    maTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trangThaiDonHang = table.Column<int>(type: "int", nullable: false),
                    ngayLap = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    diaChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.maDonHang);
                    table.ForeignKey(
                        name: "FK_DonHang_TaiKhoan",
                        column: x => x.maTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "maTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    maTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    maSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => new { x.maTaiKhoan, x.maSanPham });
                    table.ForeignKey(
                        name: "FK_GioHang_SanPham",
                        column: x => x.maSanPham,
                        principalTable: "SanPham",
                        principalColumn: "maSanPham",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHang_TaiKhoan",
                        column: x => x.maTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "maTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamYeuThich",
                columns: table => new
                {
                    maTaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    maSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamYeuThich", x => new { x.maTaiKhoan, x.maSanPham });
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThich_SanPham",
                        column: x => x.maSanPham,
                        principalTable: "SanPham",
                        principalColumn: "maSanPham",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThich_TaiKhoan",
                        column: x => x.maTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "maTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    maDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    maSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => new { x.maDonHang, x.maSanPham });
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang",
                        column: x => x.maDonHang,
                        principalTable: "DonHang",
                        principalColumn: "maDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_SanPham",
                        column: x => x.maSanPham,
                        principalTable: "SanPham",
                        principalColumn: "maSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_maSanPham",
                table: "ChiTietDonHang",
                column: "maSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_maTaiKhoan",
                table: "DonHang",
                column: "maTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_maSanPham",
                table: "GioHang",
                column: "maSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_maDanhMuc",
                table: "SanPham",
                column: "maDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThich_maSanPham",
                table: "SanPhamYeuThich",
                column: "maSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_tenTaiKhoan",
                table: "TaiKhoan",
                column: "tenTaiKhoan",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "SanPhamYeuThich");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "DanhMuc");
        }
    }
}
