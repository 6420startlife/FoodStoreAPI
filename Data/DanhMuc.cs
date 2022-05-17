namespace FoodStore_API.Data
{
    public class DanhMuc
    {
        public DanhMuc()
        {
            list_sanPham = new HashSet<SanPham>();
        }

        public Guid maDanhMuc { get; set; }
        public string tenDanhMuc { get; set; }

        public virtual ICollection<SanPham> list_sanPham { get; set; }
    }
}
