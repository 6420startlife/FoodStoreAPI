using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStore_API.Migrations
{
    public partial class AddMotaSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "moTa",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "moTa",
                table: "SanPham");
        }
    }
}
