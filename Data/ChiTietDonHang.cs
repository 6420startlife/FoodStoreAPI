namespace FoodStore_API.Data
{
    public class ChiTietDonHang
    {
        public Guid maDonHang { get; set; }
        public Guid maSanPham { get; set; }
        public int soLuong { get; set; }

        public virtual DonHang donHang_owner { get; set; }
        public virtual SanPham sanPham_owner { get; set; }
    }
}
