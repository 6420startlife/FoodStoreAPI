#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodStore_API.Data;

namespace FoodStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamYeuThichController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;

        public SanPhamYeuThichController(FoodStore_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPhamYeuThich>>> GetSanPhamYeuThich_DbSet()
        {
            return await _context.SanPhamYeuThich_DbSet.ToListAsync();
        }

        [HttpGet("{maTaiKhoan}")]
        public IActionResult GetSanPhamYeuThich(Guid maTaiKhoan)
        {
            var list = _context.SanPhamYeuThich_DbSet.Where(e => e.maTaiKhoan == maTaiKhoan).ToList();
            if(list.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(list);
            }
        }

        [HttpGet("{maTaiKhoan}/{maSanPham}")]
        public IActionResult GetSanPhamYeuThich(Guid maTaiKhoan, Guid maSanPham)
        {
            var sanPhamYeuThich = _context.SanPhamYeuThich_DbSet.Where(e => e.maTaiKhoan == maTaiKhoan && e.maSanPham == maSanPham);
            if (sanPhamYeuThich == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sanPhamYeuThich);
            }
        }

        [HttpPost("{maTaiKhoan}/{maSanPham}")]
        public IActionResult PostSanPhamYeuThich(Guid maTaiKhoan, Guid maSanPham)
        {
            var sanPhamYeuThich = new SanPhamYeuThich
            {
                maTaiKhoan = maTaiKhoan,
                maSanPham = maSanPham
            };
            var taiKhoan = CheckMaTaiKhoan(maTaiKhoan);
            var sanPham = CheckMaSanPham(maSanPham);
            if (taiKhoan == null || sanPham == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Add(sanPhamYeuThich);
                _context.SaveChanges();
                return Ok(sanPhamYeuThich);
            }
        }

        [HttpDelete("{maTaiKhoan}")]
        public IActionResult DeleteSanPhamYeuThich(Guid maTaiKhoan)
        {
            var list = _context.SanPhamYeuThich_DbSet.Where(e => e.maTaiKhoan == maTaiKhoan).ToList();
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
        public IActionResult DeleteSanPhamYeuThich(Guid maTaiKhoan, Guid maSanPham)
        {
            var sanPhamYeuThich = _context.SanPhamYeuThich_DbSet.SingleOrDefault(e => e.maTaiKhoan == maTaiKhoan && e.maSanPham == maSanPham);
            if (sanPhamYeuThich == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(sanPhamYeuThich);
                _context.SaveChanges();
                return NoContent();
            }
        }

        private TaiKhoan CheckMaTaiKhoan(Guid maTaiKhoan)
        {
            return _context.TaiKhoan_DbSet.Find(maTaiKhoan);
        }

        private SanPham CheckMaSanPham(Guid maSanPham)
        {
            return _context.SanPham_DbSet.Find(maSanPham);
        }
    }
}
