using Microsoft.EntityFrameworkCore;

namespace FoodStore_API.Data
{
    public class FoodStore_DbContext : DbContext
    {
        public FoodStore_DbContext(DbContextOptions options) : base(options)
        {
        }

        protected FoodStore_DbContext()
        {
        }

        public virtual DbSet<TaiKhoan> TaiKhoan_DbSet { get; set; }
        public virtual DbSet<DanhMuc> DanhMuc_DbSet { get; set; }
        public virtual DbSet<SanPham> SanPham_DbSet { get; set; }
        public virtual DbSet<GioHang> GioHang_DbSet { get; set; }
        public virtual DbSet<SanPhamYeuThich> SanPhamYeuThich_DbSet { get; set; }
        public virtual DbSet<DonHang> DonHang_DbSet { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHang_DbSet { get; set; }
        public virtual DbSet<PhieuGiamGia> PhieuGiamGia_DbSet { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.ToTable("TaiKhoan");
                entity.HasKey(e => e.maTaiKhoan);
                entity.HasIndex(e => e.tenTaiKhoan).IsUnique();
                entity.Property(e => e.tenTaiKhoan).IsRequired().HasMaxLength(30);
                entity.Property(e => e.matKhau).IsRequired().HasMaxLength(30);
                entity.Property(e => e.hoVaTen).IsRequired();
                entity.Property(e => e.diaChi).IsRequired();
                entity.Property(e => e.soDienThoai).HasMaxLength(20).IsRequired();
                entity.Property(e => e.ngaySinh).HasColumnType("date");
            });
            modelBuilder.Entity<DanhMuc>(entity =>
            {
                entity.ToTable("DanhMuc");
                entity.HasKey(e => e.maDanhMuc);
            });
            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.ToTable("SanPham");
                entity.HasKey(e => e.maSanPham);
                entity.HasOne(e => e.danhMuc_owner)
                    .WithMany(e => e.list_sanPham)
                    .HasForeignKey(e => e.maDanhMuc)
                    .HasConstraintName("FK_SanPham_DanhMuc");
                entity.Property(e => e.tenSanPham).IsRequired();
                entity.Property(e => e.maDanhMuc).IsRequired();
                entity.Property(e => e.trangThaiSanPham).HasDefaultValue(TrangThaiSanPham.dangMoBan);
            });
            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.ToTable("GioHang");
                entity.HasKey(e => new {e.maTaiKhoan, e.maSanPham});
                entity.HasOne(e => e.taiKhoan_owner)
                    .WithMany(e => e.list_gioHang)
                    .HasForeignKey(e => e.maTaiKhoan)
                    .HasConstraintName("FK_GioHang_TaiKhoan");
                entity.HasOne(e => e.sanPham_owner)
                    .WithMany(e => e.list_gioHang)
                    .HasForeignKey(e => e.maSanPham)
                    .HasConstraintName("FK_GioHang_SanPham");
            });
            modelBuilder.Entity<SanPhamYeuThich>(entity =>
            {
                entity.ToTable("SanPhamYeuThich");
                entity.HasKey(e => new { e.maTaiKhoan, e.maSanPham });
                entity.HasOne(e => e.taiKhoan_owner)
                    .WithMany(e => e.list_sanPhamYeuThich)
                    .HasForeignKey(e => e.maTaiKhoan)
                    .HasConstraintName("FK_SanPhamYeuThich_TaiKhoan");
                entity.HasOne(e => e.sanPham_owner)
                    .WithMany(e => e.list_sanPhamYeuThich)
                    .HasForeignKey(e => e.maSanPham)
                    .HasConstraintName("FK_SanPhamYeuThich_SanPham");
            });
            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.ToTable("DonHang");
                entity.HasKey(e => new {e.maDonHang });
                entity.Property(e => e.ngayLap).HasDefaultValueSql("getdate()");
                entity.Property(e => e.nguoiNhan).IsRequired().HasMaxLength(50);
                entity.Property(e => e.trangThaiDonHang).HasDefaultValue(TrangThaiDonHang.choDuyet);
                entity.HasOne(e => e.taiKhoan_owner)
                    .WithMany(e => e.list_donHang)
                    .HasForeignKey(e => e.maTaiKhoan)
                    .HasConstraintName("FK_DonHang_TaiKhoan");
                entity.HasOne(e => e.phieuGiamGia_owner)
                    .WithMany()
                    .HasForeignKey(e => e.maGiamGia)
                    .HasConstraintName("FK_DonHang_PhieuGiamGia");
            });
            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(e => new { e.maDonHang, e.maSanPham });

                entity.HasOne(e => e.donHang_owner)
                    .WithMany(e => e.list_chiTietDonHang)
                    .HasForeignKey(e => e.maDonHang)
                    .HasConstraintName("FK_ChiTietDonHang_DonHang");

                entity.HasOne(e => e.sanPham_owner)
                    .WithMany(e => e.list_chiTietDonHang)
                    .HasForeignKey(e => e.maSanPham)
                    .HasConstraintName("FK_ChiTietDonHang_SanPham");
            });
            modelBuilder.Entity<PhieuGiamGia>(entity =>
            {
                entity.ToTable("PhieuGiamGia");
                entity.HasKey(e => e.maPhieuGiamGia);
                entity.HasIndex(e => e.maNhap).IsUnique();
                entity.Property(e => e.maNhap).IsRequired();
                entity.Property(e => e.ngayBatDau).IsRequired();
                entity.Property(e => e.ngayKetThuc).IsRequired();
                entity.Property(e => e.soLuong).IsRequired();
            });
        }
    }
}
