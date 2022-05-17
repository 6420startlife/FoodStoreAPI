namespace FoodStore_API.Data
{
    public class GioHang
    {
        public Guid maTaiKhoan { get; set; }
        public Guid maSanPham { get; set; }
        public int soLuong { get; set; }

        public virtual TaiKhoan taiKhoan_owner { get; set; }
        public virtual SanPham sanPham_owner { get; set; }
    }
}
