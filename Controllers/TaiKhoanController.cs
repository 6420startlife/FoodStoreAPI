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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FoodStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly FoodStore_DbContext _context;
        private readonly AppSetting _appSetting;

        public TaiKhoanController(FoodStore_DbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSetting = optionsMonitor.CurrentValue;
        }

        // GET: api/TaiKhoan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaiKhoan>>> GetTaiKhoan_DbSet()
        {
            return await _context.TaiKhoan_DbSet.ToListAsync();
        }

        [HttpGet("{maTaiKhoan}")]
        public async Task<ActionResult<TaiKhoan>> GetTaiKhoan(Guid maTaiKhoan)
        {
            var taiKhoan = await _context.TaiKhoan_DbSet.FindAsync(maTaiKhoan);

            if (taiKhoan == null)
            {
                return NotFound();
            }

            return taiKhoan;
        }

        [HttpGet("GetTaiKhoanByLogin/{username}/{password}")]
        public IActionResult GetTaiKhoanByLogin(string username, string password)
        {
            var taiKhoan = _context.TaiKhoan_DbSet.SingleOrDefault(e => e.tenTaiKhoan == username && e.matKhau == password);
            if(taiKhoan == null)
            {
                return NotFound();
            }
            return Ok(taiKhoan);
        }

        [HttpPost("Login")]
        public IActionResult Validate(TaiKhoan_Login login)
        {
            var taiKhoan = _context.TaiKhoan_DbSet.SingleOrDefault(e => e.tenTaiKhoan == login.tenTaiKhoan && e.matKhau == login.matKhau);
            if(taiKhoan == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid account"
                });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authentication Success",
                Data = GenerateToken(taiKhoan)
            });
        }

        private string GenerateToken(TaiKhoan taiKhoan)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var TokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                        new Claim (ClaimTypes.Name, taiKhoan.hoVaTen),
                        new Claim ("ID", taiKhoan.maTaiKhoan.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, taiKhoan.tenTaiKhoan),
                        //roles

                        new Claim("TokenId", Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(TokenDecriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [HttpPut("{maTaiKhoan}")]
        public IActionResult PutTaiKhoan(Guid maTaiKhoan, TaiKhoan taiKhoan)
        {
            if (maTaiKhoan != taiKhoan.maTaiKhoan)
            {
                return BadRequest();
            }

            _context.Entry(taiKhoan).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaiKhoanExists(maTaiKhoan))
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

        [HttpPost]
        public IActionResult PostTaiKhoan(TaiKhoan_Model model)
        {
            try
            {
                TaiKhoan taiKhoan = new TaiKhoan
                {
                    tenTaiKhoan = model.tenTaiKhoan,
                    matKhau = model.matKhau,
                    trangThai = model.trangThai,
                    hoVaTen = model.hoVaTen,
                    quyenTaiKhoan = model.quyenTaiKhoan,
                    ngaySinh = model.ngaySinh,
                    anhDaiDien = model.anhDaiDien,
                    diaChi = model.diaChi,
                    soDienThoai = model.soDienThoai,
                    email = model.email,
                    list_donHang = new HashSet<DonHang>(),
                    list_gioHang = new HashSet<GioHang>(),
                    list_sanPhamYeuThich = new HashSet<SanPhamYeuThich>()
                };
                _context.Add(taiKhoan);
                _context.SaveChanges();
                return Ok(taiKhoan);
            }
            catch
            {
                return BadRequest();
            } 
        }

        [HttpDelete("{maTaiKhoan}")]
        public IActionResult DeleteTaiKhoan(Guid maTaiKhoan)
        {
            var taiKhoan = _context.TaiKhoan_DbSet.Find(maTaiKhoan);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            _context.TaiKhoan_DbSet.Remove(taiKhoan);
            _context.SaveChanges();

            return NoContent();
        }

        private bool TaiKhoanExists(Guid maTaiKhoan)
        {
            return _context.TaiKhoan_DbSet.Any(e => e.maTaiKhoan == maTaiKhoan);
        }

        [HttpGet("CheckTaiKhoan/{username}")]
        public IActionResult CheckTaiKhoan(string username)
        {
            var taiKhoan = _context.TaiKhoan_DbSet.SingleOrDefault(e => e.tenTaiKhoan == username);
            if(taiKhoan == null)
            {
                return NotFound();
            }
            return Ok(taiKhoan.maTaiKhoan);
        }
        [HttpGet("CheckEmail/{username}/{email}")]
        public IActionResult CheckEmail(string username, string email)
        {
            var taiKhoan = _context.TaiKhoan_DbSet.Any(e => e.tenTaiKhoan == username && e.email == email);
            if(taiKhoan == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Tồn tại username có email trên",
                Data = taiKhoan
                
            });
        }
    }
}
