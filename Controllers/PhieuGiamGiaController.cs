using FoodStore_API.Data;
using FoodStore_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuGiamGiaController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;

        public PhieuGiamGiaController(FoodStore_DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuGiamGia>>> GetPhieuGiamGia_DbSet()
        {
            return await _context.PhieuGiamGia_DbSet.Where(e => e.ngayBatDau < DateTime.Now && e.ngayKetThuc > DateTime.Now).ToListAsync();
        }

        [HttpPost]
        public IActionResult PostPhieuGiamGia(PhieuGiamGia_Model model)
        {
            if (CheckMaGiamGiaExists(model.maNhap))
            {
                return BadRequest();
            }
            if(model == null)
            {
                return BadRequest();
            }
            var phieuGiamGia = new PhieuGiamGia
            {
                maNhap = model.maNhap,
                tenPhieuGiamGia = model.tenPhieuGiamGia,
                ngayBatDau = model.ngayBatDau,
                ngayKetThuc = model.ngayKetThuc,
                phanTramGiam = model.phanTramGiam,
                soLuong = model.soLuong
            };
            _context.Add(phieuGiamGia);
            _context.SaveChanges();
            return Ok(phieuGiamGia);
        }

        [HttpPut]
        public async Task<IActionResult> PutPhieuGiamGia(Guid maPhieuGiamGia,PhieuGiamGia phieuGiamGia)
        {
            if(maPhieuGiamGia != phieuGiamGia.maPhieuGiamGia)
            {
                return BadRequest();
            }
            _context.Entry(phieuGiamGia).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exist = _context.PhieuGiamGia_DbSet.Any(e => e.maPhieuGiamGia == maPhieuGiamGia);
                if (!exist)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeletePhieuGiamGia(Guid maPhieuGiamGia)
        {
            var phieuGiamGia = _context.PhieuGiamGia_DbSet.Find(maPhieuGiamGia.ToString());
            if(phieuGiamGia == null)
            {
                return NotFound();
            }
            _context.Remove(phieuGiamGia);
            _context.SaveChanges();
            return NoContent();
        }

        private bool CheckMaGiamGiaExists(string maNhap)
        {
            return _context.PhieuGiamGia_DbSet.Any(e => e.maNhap == maNhap);
        }

        [HttpGet("{maNhap}")]
        public IActionResult getPhieuGiamGia(string maNhap)
        {
            var phieuGiamGia = _context.PhieuGiamGia_DbSet.SingleOrDefault(e => e.maNhap == maNhap);
            if(phieuGiamGia == null)
            {
                return NotFound();
            }
            return Ok(phieuGiamGia);
        }

        [HttpGet("find/{maPhieuGiamGia}")]
        public IActionResult getPhieuGiamGia(Guid maPhieuGiamGia)
        {
            var phieuGiamGia = _context.PhieuGiamGia_DbSet.SingleOrDefault(e => e.maPhieuGiamGia == maPhieuGiamGia);
            if (phieuGiamGia == null)
            {
                return NotFound();
            }
            return Ok(phieuGiamGia);
        }
    }
}
