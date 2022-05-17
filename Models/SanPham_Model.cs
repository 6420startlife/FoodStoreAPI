using System.ComponentModel.DataAnnotations;

namespace FoodStore_API.Models
{
    public class SanPham_Model
    {
        public Guid maDanhMuc { get; set; }
        public string tenSanPham { get; set; }
        [Range(0, double.MaxValue)]
        public double giaSanPham { get; set; }
        [Range(0, int.MaxValue)]
        public int soLuongTonKho { get; set; }
        public string anhSanPham { get; set; }
        public string moTa { get; set; }
    }
}
