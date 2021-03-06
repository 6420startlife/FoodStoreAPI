using System.ComponentModel.DataAnnotations;

namespace FoodStore_API.Models
{
    public class PhieuGiamGia_Model
    {
        public string tenPhieuGiamGia { get; set; }
        public string maNhap { get; set; }
        public DateTime ngayBatDau { get; set; }
        public DateTime ngayKetThuc { get; set; }
        [Range(0, 100)]
        public int phanTramGiam { get; set; }
        [Range(0, int.MaxValue)]
        public int soLuong { get; set; }
    }
}
