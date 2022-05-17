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
    public class DanhMucController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;

        public DanhMucController(FoodStore_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhMuc>>> GetDanhMuc_DbSet()
        {
            return await _context.DanhMuc_DbSet.ToListAsync();
        }

        [HttpGet("{maDanhMuc}")]
        public IActionResult GetDanhMuc(Guid maDanhMuc)
        {
            var danhMuc = _context.DanhMuc_DbSet.Find(maDanhMuc);

            if (danhMuc == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(danhMuc);
            }
        }

        [HttpPut("{maDanhMuc}")]
        public IActionResult PutDanhMuc(Guid maDanhMuc, DanhMuc_Model model)
        {
            try
            {
                var danhMuc = _context.DanhMuc_DbSet.SingleOrDefault(e => e.maDanhMuc == maDanhMuc);
                if(danhMuc == null)
                {
                    return NotFound();
                }
                danhMuc.tenDanhMuc = model.tenDanhMuc;
                _context.DanhMuc_DbSet.Update(danhMuc);
                _context.SaveChanges();
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult PostDanhMuc(DanhMuc_Model model)
        {
            try
            {
                DanhMuc danhMuc = new DanhMuc
                {
                    tenDanhMuc = model.tenDanhMuc,
                    list_sanPham = new HashSet<SanPham>()
                };
                _context.Add(danhMuc);
                _context.SaveChanges();
                return Ok(danhMuc);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{maDanhMuc}")]
        public IActionResult DeleteDanhMuc(Guid maDanhMuc)
        {
            var danhMuc = _context.DanhMuc_DbSet.SingleOrDefault(e => e.maDanhMuc == maDanhMuc);
            if (danhMuc == null)
            {
                return NotFound();
            }
            _context.Remove(danhMuc);
            _context.SaveChanges();
            return NoContent();
        }

        private bool DanhMucExists(Guid maDanhMuc)
        {
            return _context.DanhMuc_DbSet.Any(e => e.maDanhMuc == maDanhMuc);
        }
    }
}
