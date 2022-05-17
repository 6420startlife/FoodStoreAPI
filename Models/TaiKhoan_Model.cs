using FoodStore_API.Data;

namespace FoodStore_API.Models
{
    public class TaiKhoan_Model
    {
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
    }
}
