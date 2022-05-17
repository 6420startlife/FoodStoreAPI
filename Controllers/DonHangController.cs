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
    public class DonHangController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;

        public DonHangController(FoodStore_DbContext context)
        {
            _context = context;
        }

        // GET: api/DonHang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetDonHang_DbSet()
        {
            return await _context.DonHang_DbSet.ToListAsync();
        }

        // GET: api/DonHang/5
        [HttpGet("{maDonHang}")]
        public async Task<ActionResult<DonHang>> GetDonHang(Guid maDonHang)
        {
            var donHang = await _context.DonHang_DbSet.FindAsync(maDonHang);

            if (donHang == null)
            {
                return NotFound();
            }

            return donHang;
        }

        [HttpGet("GetDonHangByMaTaiKhoan/{maTaiKhoan}")]
        public IActionResult GetDonHangByMaTaiKhoan(Guid maTaiKhoan)
        {
            return Ok(_context.DonHang_DbSet.Where(e => e.maTaiKhoan == maTaiKhoan).ToList());
        }

        // PUT: api/DonHang/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{maDonHang}")]
        public async Task<IActionResult> PutDonHang(Guid maDonHang, DonHang donHang)
        {
            if (maDonHang != donHang.maDonHang)
            {
                return BadRequest();
            }

            _context.Entry(donHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonHangExists(maDonHang))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DonHang
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostDonHang(DonHang_Model model)
        {
            var donHang = new DonHang
            {
                maTaiKhoan = model.maTaiKhoan,
                nguoiNhan = model.nguoiNhan,
                diaChi = model.diaChi,
                maGiamGia = model.maGiamGia,
                list_chiTietDonHang = new HashSet<ChiTietDonHang>(),
            };
            var taiKhoan = CheckMaTaiKhoan(model.maTaiKhoan);
            if(taiKhoan == null)
            {
                return BadRequest();
            }
            else
            {
                donHang.taiKhoan_owner = taiKhoan;
                _context.Add(donHang);
                _context.SaveChanges();
                return Ok(donHang);
            }
        }

        // DELETE: api/DonHang/5
        [HttpDelete("{maDonHang}")]
        public async Task<IActionResult> DeleteDonHang(Guid maDonHang)
        {
            var donHang = await _context.DonHang_DbSet.FindAsync(maDonHang);
            if (donHang == null)
            {
                return NotFound();
            }

            _context.DonHang_DbSet.Remove(donHang);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonHangExists(Guid id)
        {
            return _context.DonHang_DbSet.Any(e => e.maDonHang == id);
        }
        private TaiKhoan CheckMaTaiKhoan(Guid maTaiKhoan)
        {
            return _context.TaiKhoan_DbSet.Find(maTaiKhoan);
        }

        private bool CheckMaTaiKhoan_bool(Guid maTaiKhoan)
        {
            return _context.TaiKhoan_DbSet.Any(e => e.maTaiKhoan == maTaiKhoan);
        }

        [HttpPost("ChuyenGioHangThanhDonHang")]
        public IActionResult ChuyenGioHangThanhDonHang(DonHang_Model model)
        {
            if (CheckMaTaiKhoan_bool(model.maTaiKhoan))
            {
                var list_gioHang = _context.GioHang_DbSet.Where(e => e.maTaiKhoan == model.maTaiKhoan).ToList();
                if(list_gioHang.Count > 0)
                {
                    var donHang = new DonHang
                    {
                        maTaiKhoan = model.maTaiKhoan,
                        nguoiNhan = model.nguoiNhan,
                        diaChi = model.diaChi,
                        maGiamGia = model.maGiamGia,
                        taiKhoan_owner = CheckMaTaiKhoan(model.maTaiKhoan),
                        list_chiTietDonHang = new HashSet<ChiTietDonHang>()
                    };
                    try
                    {
                        donHang.list_chiTietDonHang = GetDataFromOtherListGioHang(donHang.maDonHang, list_gioHang);
                        _context.Add(donHang);
                        _context.RemoveRange(list_gioHang);
                        _context.SaveChanges();
                        return NoContent();
                    }
                    catch
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        private List<ChiTietDonHang> GetDataFromOtherListGioHang(Guid maDonHang,List<GioHang> list_gioHang)
        {
            List<ChiTietDonHang> list_chiTietDonHang = new List<ChiTietDonHang>();
            foreach(GioHang gioHang in list_gioHang)
            {
                ChiTietDonHang chiTietDonHang = new ChiTietDonHang
                {
                    maDonHang = maDonHang,
                    maSanPham = gioHang.maSanPham,
                    soLuong = gioHang.soLuong
                };
                list_chiTietDonHang.Add(chiTietDonHang);
            }
            return list_chiTietDonHang;
        }
    }
}
