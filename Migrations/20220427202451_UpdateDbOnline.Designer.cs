﻿// <auto-generated />
using System;
using FoodStore_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodStore_API.Migrations
{
    [DbContext(typeof(FoodStore_DbContext))]
    [Migration("20220427202451_UpdateDbOnline")]
    partial class UpdateDbOnline
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FoodStore_API.Data.ChiTietDonHang", b =>
                {
                    b.Property<Guid>("maDonHang")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("maSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("soLuong")
                        .HasColumnType("int");

                    b.HasKey("maDonHang", "maSanPham");

                    b.HasIndex("maSanPham");

                    b.ToTable("ChiTietDonHang", (string)null);
                });

            modelBuilder.Entity("FoodStore_API.Data.DanhMuc", b =>
                {
                    b.Property<Guid>("maDanhMuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("tenDanhMuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("maDanhMuc");

                    b.ToTable("DanhMuc", (string)null);
                });

            modelBuilder.Entity("FoodStore_API.Data.DonHang", b =>
                {
                    b.Property<Guid>("maDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("diaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("maTaiKhoan")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ngayLap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("nguoiNhan")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("trangThaiDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("maDonHang");

                    b.HasIndex("maTaiKhoan");

                    b.ToTable("DonHang", (string)null);
                });

            modelBuilder.Entity("FoodStore_API.Data.GioHang", b =>
                {
                    b.Property<Guid>("maTaiKhoan")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("maSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("soLuong")
                        .HasColumnType("int");

                    b.HasKey("maTaiKhoan", "maSanPham");

                    b.HasIndex("maSanPham");

                    b.ToTable("GioHang", (string)null);
                });

            modelBuilder.Entity("FoodStore_API.Data.SanPham", b =>
                {
                    b.Property<Guid>("maSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("anhSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("giaSanPham")
                        .HasColumnType("float");

                    b.Property<int>("giamGia")
                        .HasColumnType("int");

                    b.Property<Guid>("maDanhMuc")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("soLuongTonKho")
                        .HasColumnType("int");

                    b.Property<string>("tenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("trangThaiSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("maSanPham");

                    b.HasIndex("maDanhMuc");

                    b.ToTable("SanPham", (string)null);
                });

            modelBuilder.Entity("FoodStore_API.Data.SanPhamYeuThich", b =>
                {
                    b.Property<Guid>("maTaiKhoan")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("maSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("maTaiKhoan", "maSanPham");

                    b.HasIndex("maSanPham");

                    b.ToTable("SanPhamYeuThich", (string)null);
                });

            modelBuilder.Entity("FoodStore_API.Data.TaiKhoan", b =>
                {
                    b.Property<Guid>("maTaiKhoan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("anhDaiDien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("diaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hoVaTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("matKhau")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("ngaySinh")
                        .HasColumnType("date");

                    b.Property<int>("quyenTaiKhoan")
                        .HasColumnType("int");

                    b.Property<string>("soDienThoai")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("tenTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("trangThai")
                        .HasColumnType("int");

                    b.HasKey("maTaiKhoan");

                    b.HasIndex("tenTaiKhoan")
                        .IsUnique();

                    b.ToTable("TaiKhoan", (string)null);
                });

            modelBuilder.Entity("FoodStore_API.Data.ChiTietDonHang", b =>
                {
                    b.HasOne("FoodStore_API.Data.DonHang", "donHang_owner")
                        .WithMany("list_chiTietDonHang")
                        .HasForeignKey("maDonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ChiTietDonHang_DonHang");

                    b.HasOne("FoodStore_API.Data.SanPham", "sanPham_owner")
                        .WithMany("list_chiTietDonHang")
                        .HasForeignKey("maSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ChiTietDonHang_SanPham");

                    b.Navigation("donHang_owner");

                    b.Navigation("sanPham_owner");
                });

            modelBuilder.Entity("FoodStore_API.Data.DonHang", b =>
                {
                    b.HasOne("FoodStore_API.Data.TaiKhoan", "taiKhoan_owner")
                        .WithMany("list_donHang")
                        .HasForeignKey("maTaiKhoan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_DonHang_TaiKhoan");

                    b.Navigation("taiKhoan_owner");
                });

            modelBuilder.Entity("FoodStore_API.Data.GioHang", b =>
                {
                    b.HasOne("FoodStore_API.Data.SanPham", "sanPham_owner")
                        .WithMany("list_gioHang")
                        .HasForeignKey("maSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GioHang_SanPham");

                    b.HasOne("FoodStore_API.Data.TaiKhoan", "taiKhoan_owner")
                        .WithMany("list_gioHang")
                        .HasForeignKey("maTaiKhoan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GioHang_TaiKhoan");

                    b.Navigation("sanPham_owner");

                    b.Navigation("taiKhoan_owner");
                });

            modelBuilder.Entity("FoodStore_API.Data.SanPham", b =>
                {
                    b.HasOne("FoodStore_API.Data.DanhMuc", "danhMuc_owner")
                        .WithMany("list_sanPham")
                        .HasForeignKey("maDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_SanPham_DanhMuc");

                    b.Navigation("danhMuc_owner");
                });

            modelBuilder.Entity("FoodStore_API.Data.SanPhamYeuThich", b =>
                {
                    b.HasOne("FoodStore_API.Data.SanPham", "sanPham_owner")
                        .WithMany("list_sanPhamYeuThich")
                        .HasForeignKey("maSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_SanPhamYeuThich_SanPham");

                    b.HasOne("FoodStore_API.Data.TaiKhoan", "taiKhoan_owner")
                        .WithMany("list_sanPhamYeuThich")
                        .HasForeignKey("maTaiKhoan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_SanPhamYeuThich_TaiKhoan");

                    b.Navigation("sanPham_owner");

                    b.Navigation("taiKhoan_owner");
                });

            modelBuilder.Entity("FoodStore_API.Data.DanhMuc", b =>
                {
                    b.Navigation("list_sanPham");
                });

            modelBuilder.Entity("FoodStore_API.Data.DonHang", b =>
                {
                    b.Navigation("list_chiTietDonHang");
                });

            modelBuilder.Entity("FoodStore_API.Data.SanPham", b =>
                {
                    b.Navigation("list_chiTietDonHang");

                    b.Navigation("list_gioHang");

                    b.Navigation("list_sanPhamYeuThich");
                });

            modelBuilder.Entity("FoodStore_API.Data.TaiKhoan", b =>
                {
                    b.Navigation("list_donHang");

                    b.Navigation("list_gioHang");

                    b.Navigation("list_sanPhamYeuThich");
                });
#pragma warning restore 612, 618
        }
    }
}
