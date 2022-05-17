#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodStore_API.Data;
using FoodStore_API.Models;

namespace FoodStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;

        public SanPhamController(FoodStore_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPham>>> GetSanPham_DbSet()
        {
            return await _context.SanPham_DbSet.ToListAsync();
        }

        [HttpGet("{maSanPham}")]
        public IActionResult GetSanPham(Guid maSanPham)
        {
            var sanPham = _context.SanPham_DbSet.Find(maSanPham);

            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(sanPham);
        }

        [HttpPut("{maSanPham}")]
        public IActionResult PutSanPham(Guid maSanPham, SanPham_Model_Edit model)
        {
            var sanPham = _context.SanPham_DbSet.SingleOrDefault(e => e.maSanPham == maSanPham);
            if(sanPham == null)
            {
                return NotFound();
            }
            sanPham.tenSanPham = model.tenSanPham;
            sanPham.anhSanPham = model.anhSanPham;
            sanPham.giaSanPham = model.giaSanPham;
            sanPham.trangThaiSanPham = model.trangThaiSanPham;
            sanPham.soLuongTonKho = model.soLuongTonKho;
            sanPham.moTa = model.moTa;
            _context.Update(sanPham);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpPost]
        public IActionResult PostSanPham(SanPham_Model model)
        {
            var sanPham = new SanPham
            {
                maDanhMuc = model.maDanhMuc,
                tenSanPham = model.tenSanPham,
                giaSanPham = model.giaSanPham,
                soLuongTonKho = model.soLuongTonKho,
                anhSanPham = model.anhSanPham,
                moTa = model.moTa,
                danhMuc_owner = null,
                list_chiTietDonHang = null,
                list_gioHang = null,
                list_sanPhamYeuThich = null
            };
            var danhMuc = CheckMaDanhMuc(model.maDanhMuc);
            if (danhMuc == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Add(sanPham);
                _context.SaveChanges();
                return Ok(sanPham);
            }
        }

        private DanhMuc CheckMaDanhMuc(Guid maDanhMuc)
        {
            return _context.DanhMuc_DbSet.Find(maDanhMuc);
        }

        [HttpDelete("{maSanPham}")]
        public IActionResult DeleteSanPham(Guid maSanPham)
        {
            var sanPham = _context.SanPham_DbSet.Find(maSanPham);
            if (sanPham == null)
            {
                return NotFound();
            }

            _context.SanPham_DbSet.Remove(sanPham);
            _context.SaveChanges();

            return NoContent();
        }

        private bool SanPhamExists(Guid id)
        {
            return _context.SanPham_DbSet.Any(e => e.maSanPham == id);
        }

        [HttpGet("GetSanPhamByMaDanhMuc/{maDanhMuc}")]
        public async Task<ActionResult<IEnumerable<SanPham>>> GetSanPhamByMaDanhMuc(Guid maDanhMuc)
        {
            return await _context.SanPham_DbSet.Where(e => e.maDanhMuc == maDanhMuc).ToListAsync();
        }

        [HttpGet("GetSanPhamByTen/{tenSanPham}")]
        public async Task<ActionResult<IEnumerable<SanPham>>> GetSanPhamByTen(string tenSanPham)
        {
            return await _context.SanPham_DbSet.Where(e => e.tenSanPham.Contains(tenSanPham)).ToListAsync();
        }

        [HttpGet("GetSanPhamYeuThich/{maTaiKhoan}")]
        public async Task<ActionResult<SanPham>> GetSanPhamYeuThichByMaTaiKhoan(Guid maTaiKhoan)
        {
            var list = from sanPham in _context.SanPham_DbSet
                       from gioHang in _context.SanPhamYeuThich_DbSet
                       where gioHang.maTaiKhoan == maTaiKhoan && gioHang.maSanPham == sanPham.maSanPham
                       select (sanPham);
            return Ok(list);
        }

        [HttpGet("GetSanPhamTrongGioHang/{maTaiKhoan}")]
        public IActionResult GetSanPhamTrongGioHangByMaTaiKhoan(Guid maTaiKhoan)
        {
            var returnList = from sanPham in _context.SanPham_DbSet
                             from gioHang in _context.GioHang_DbSet
                             where  gioHang.maTaiKhoan == maTaiKhoan && gioHang.maSanPham == sanPham.maSanPham
                             select new { sanPham, gioHang.soLuong };
            return Ok(returnList);
        }
    }
}
