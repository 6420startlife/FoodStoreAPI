using System.ComponentModel.DataAnnotations;

namespace FoodStore_API.Models
{
    public class TaiKhoan_Login
    {
        [Required]
        [MaxLength(50)]
        public string tenTaiKhoan { get; set; }
        [Required]
        [MaxLength(200)]
        public string matKhau { get; set; }
    }
}
