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
    public class GioHangController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;

        public GioHangController(FoodStore_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GioHang>>> GetGioHang_DbSet()
        {
            return await _context.GioHang_DbSet.ToListAsync();
        }

        [HttpGet("{maTaiKhoan}")]
        public IActionResult GetGioHang(Guid maTaiKhoan)
        {
            var list = _context.GioHang_DbSet.Where(e => e.maTaiKhoan == maTaiKhoan).ToList();
            if (list.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(list);
            }
        }

        [HttpGet("{maTaiKhoan}/{maSanPham}")]
        public IActionResult GetGioHang(Guid maTaiKhoan, Guid maSanPham)
        {
            var gioHang = _context.GioHang_DbSet.SingleOrDefault(e => e.maTaiKhoan == maTaiKhoan && e.maSanPham == maSanPham);
            if (gioHang == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(gioHang);
            }
        }

        [HttpPut("{maTaiKhoan}/{maSanPham}")]
        public IActionResult PutGioHang(Guid maTaiKhoan,Guid maSanPham, GioHang_Model model)
        {
            var gioHang = new GioHang
            {
                maTaiKhoan = maTaiKhoan,
                maSanPham = maSanPham,
                soLuong = model.soLuong
            };
            if (!GioHangExists(maTaiKhoan,maSanPham))
            {
                return NotFound();
            }
            else
            {
                _context.Update(gioHang);
                _context.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost("{maTaiKhoan}/{maSanPham}")]
        public IActionResult PostGioHang(Guid maTaiKhoan, Guid maSanPham, GioHang_Model model)
        {
            if(CheckMaTaiKhoan(maTaiKhoan) && CheckMaSanPham(maSanPham))
            {
                var gioHang = new GioHang
                {
                    maTaiKhoan = maTaiKhoan,
                    maSanPham = maSanPham,
                    soLuong = model.soLuong
                };
                _context.Add(gioHang);
                _context.SaveChanges();
                return Ok(gioHang);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{maTaiKhoan}")]
        public IActionResult DeleteGioHang(Guid maTaiKhoan)
        {
            var list = _context.GioHang_DbSet.Where(e => e.maTaiKhoan == maTaiKhoan).ToList();
            if(list.Count == 0)
            {
                return NotFound();
            }
            else
            {
                _context.RemoveRange(list);
                _context.SaveChanges();
                return NoContent();
            }
        }

        [HttpDelete("{maTaiKhoan}/{maSanPham}")]
        public IActionResult DeleteGioHang(Guid maTaiKhoan, Guid maSanPham)
        {
            var gioHang = _context.GioHang_DbSet.SingleOrDefault(e => e.maTaiKhoan == maTaiKhoan && e.maSanPham == maSanPham);
            if (gioHang == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(gioHang);
                _context.SaveChanges();
                return NoContent();
            }
        }

        private bool GioHangExists(Guid maTaiKhoan, Guid maSanPham)
        {
            return _context.GioHang_DbSet.Any(e => e.maTaiKhoan == maTaiKhoan && e.maSanPham == maSanPham);
        }
        private bool CheckMaTaiKhoan(Guid maTaiKhoan)
        {
            return _context.TaiKhoan_DbSet.Any(e => e.maTaiKhoan == maTaiKhoan);
        }

        private bool CheckMaSanPham(Guid maSanPham)
        {
            return _context.SanPham_DbSet.Any(e => e.maSanPham == maSanPham);
        }
    }
}
