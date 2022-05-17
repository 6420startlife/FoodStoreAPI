using System.ComponentModel.DataAnnotations;

namespace FoodStore_API.Data
{
    public enum TrangThaiSanPham
    {
        hetHang = 0,
        dangMoBan = 1
    }
    public class SanPham
    {
        public SanPham()
        {
            list_gioHang = new HashSet<GioHang>();
            list_chiTietDonHang = new HashSet<ChiTietDonHang>();
            list_sanPhamYeuThich = new HashSet<SanPhamYeuThich>();
        }

        public Guid maSanPham { get; set; }
        public string tenSanPham { get; set; }
        [Range(0, double.MaxValue)]
        public double giaSanPham { get; set; }
        public TrangThaiSanPham trangThaiSanPham { get; set; }
        [Range(0,int.MaxValue)]
        public int soLuongTonKho { get; set; }
        public string anhSanPham { get; set; }
        public Guid maDanhMuc { get; set; }
        public string moTa { get; set; }

        public virtual DanhMuc danhMuc_owner { get; set; }
        public virtual ICollection<GioHang> list_gioHang { get; set; }
        public virtual ICollection<ChiTietDonHang> list_chiTietDonHang { get; set; }
        public virtual ICollection<SanPhamYeuThich> list_sanPhamYeuThich { get; set; }
    }
}
