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
    public class ChiTietDonHangController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;

        public ChiTietDonHangController(FoodStore_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChiTietDonHang>>> GetChiTietDonHang_DbSet()
        {
            return await _context.ChiTietDonHang_DbSet.ToListAsync();
        }

        [HttpGet("{maDonHang}")]
        public IActionResult GetChiTietDonHang(Guid maDonHang)
        {
            var list = from ctdh in _context.ChiTietDonHang_DbSet
                       from sanPham in _context.SanPham_DbSet
                       where ctdh.maDonHang == maDonHang && sanPham.maSanPham == ctdh.maSanPham
                       select new { ctdh.soLuong, sanPham };
            if (list.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(list);
            }
        }

        [HttpGet("{maDonHang}/{maSanPham}")]
        public IActionResult GetChiTietDonHang(Guid maDonHang, Guid maSanPham)
        {
            var chiTietDonHang = _context.ChiTietDonHang_DbSet.SingleOrDefault(e => e.maDonHang == maDonHang && e.maSanPham == maSanPham);
            if (chiTietDonHang == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(chiTietDonHang);
            }
        }

        [HttpPut("{maDonHang}/{maSanPham}")]
        public IActionResult PutChiTietDonHang(Guid maDonHang, Guid maSanPham, ChiTietDonHang_Model model)
        {
            var chiTietDonHang = new ChiTietDonHang
            {
                maDonHang = maDonHang,
                maSanPham = maSanPham,
                soLuong = model.soLuong
            };
            if (ChiTietDonHangExists(maDonHang,maSanPham))
            {
                _context.Update(chiTietDonHang);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{maDonHang}/{maSanPham}")]
        public IActionResult PostChiTietDonHang(Guid maDonHang,Guid maSanPham,ChiTietDonHang_Model model)
        {
            var chiTietDonHang = new ChiTietDonHang {
                maDonHang = maDonHang,
                maSanPham = maSanPham,
                soLuong = model.soLuong
            };
            var donHang = CheckMaDonHang(maDonHang);
            var sanPham = CheckMaSanPham(maSanPham);
            if (donHang == null || sanPham == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Add(chiTietDonHang);
                _context.SaveChanges();
                return Ok(chiTietDonHang);
            }
        }

        [HttpDelete("{maDonHang}")]
        public IActionResult DeleteChiTietDonHang(Guid maDonHang)
        {
            var list = _context.ChiTietDonHang_DbSet.Where(e => e.maDonHang == maDonHang).ToList();
            if (list.Count > 0)
            {   _context.RemoveRange(list);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{maDonHang}/{maSanPham}")]
        public IActionResult DeleteChiTietDonHang(Guid maDonHang, Guid maSanPham)
        {
            var chiTietDonHang = _context.ChiTietDonHang_DbSet.SingleOrDefault(e => e.maDonHang == maDonHang && e.maSanPham == maSanPham);
            if (chiTietDonHang != null)
            {
                _context.Remove(chiTietDonHang);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        private bool ChiTietDonHangExists(Guid maDonHang, Guid maSanPham)
        {
            var chiTietDonHang = _context.ChiTietDonHang_DbSet.SingleOrDefault(e => e.maDonHang == maDonHang && e.maSanPham == maSanPham);
            if (chiTietDonHang == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private SanPham CheckMaSanPham(Guid maSanPham)
        {
            return _context.SanPham_DbSet.Find(maSanPham);
        }

        private DonHang CheckMaDonHang(Guid maDonHang)
        {
            return _context.DonHang_DbSet.Find(maDonHang);
        }
    }
}
