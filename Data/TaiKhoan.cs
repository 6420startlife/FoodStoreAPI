namespace FoodStore_API.Data
{
    public enum TrangThaiKhoa
    {
        moKhoa = 0, khoa = 1
    }
    public enum QuyenTaiKhoan
    {
        admin = 0, user = 1
    }
    public class TaiKhoan
    {
        public TaiKhoan()
        {
            list_gioHang = new HashSet<GioHang>();
            list_sanPhamYeuThich = new HashSet<SanPhamYeuThich>();
            list_donHang = new HashSet<DonHang>();
        }

        public Guid maTaiKhoan { get; set; }
        public string tenTaiKhoan { get; set; }
        public string matKhau { get; set; }
        public TrangThaiKhoa trangThai { get; set; }
        public string hoVaTen { get; set; }
        public QuyenTaiKhoan quyenTaiKhoan { get; set; }
        public string anhDaiDien { get; set; }
        public string diaChi { get; set; }
        public DateTime? ngaySinh { get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }

        public virtual ICollection<GioHang> list_gioHang { get; set; }
        public virtual ICollection<SanPhamYeuThich> list_sanPhamYeuThich { get; set; }
        public virtual ICollection<DonHang> list_donHang { get; set; }
    }
}
