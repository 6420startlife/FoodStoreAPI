using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStore_API.Migrations
{
    public partial class AddRelationshipPhieuGiamGia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "maGiamGia",
                table: "DonHang",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_maGiamGia",
                table: "DonHang",
                column: "maGiamGia");

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_PhieuGiamGia",
                table: "DonHang",
                column: "maGiamGia",
                principalTable: "PhieuGiamGia",
                principalColumn: "maPhieuGiamGia",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_PhieuGiamGia",
                table: "DonHang");

            migrationBuilder.DropIndex(
                name: "IX_DonHang_maGiamGia",
                table: "DonHang");

            migrationBuilder.AlterColumn<string>(
                name: "maGiamGia",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
