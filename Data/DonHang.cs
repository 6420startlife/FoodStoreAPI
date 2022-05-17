namespace FoodStore_API.Data
{
    public enum TrangThaiDonHang
    {
        choDuyet = 0,
        daDuyet = 1
    }
    public class DonHang
    {
        public DonHang()
        {
            list_chiTietDonHang = new HashSet<ChiTietDonHang>();
        }

        public Guid maDonHang { get; set; }
        public Guid maTaiKhoan { get; set; }
        public TrangThaiDonHang trangThaiDonHang { get; set; }
        public DateTime ngayLap { get; set; }
        public string nguoiNhan { get; set; }
        public string diaChi { get; set; }
        public Guid maGiamGia { get; set; }
        public virtual PhieuGiamGia phieuGiamGia_owner { get; set; }
        public virtual TaiKhoan taiKhoan_owner { get; set; }
        public virtual ICollection<ChiTietDonHang> list_chiTietDonHang { get; set; }
    }
}
