using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Data;
using ProductApp.DTOs;
using ProductApp.Model;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(AppDbContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Username = request.Username,
                Passwordhash = passwordHash,
                Role = "User" // نقش پیش‌فرض
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);

        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            // ۱. پیدا کردن کاربر بر اساس نام کاربری
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            // ۲. اگر کاربر پیدا نشد
            if (user == null)
            {
                return BadRequest("کاربر پیدا نشد! (شاید هنوز ثبت‌نام نکردی احمد جان؟)");
            }

            // ۳. چک کردن پسورد (مقایسه پسورد ارسالی با هش ذخیره شده در دیتابیس)
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Passwordhash))
            {
                return BadRequest("رمز عبور اشتباه است! دوباره تلاش کن.");
            }

            // تولید توکن و ارسال آن به کاربر
            string token = CreateToken(user);
            return Ok(token);
        }


        //Create Token

        private string CreateToken(User user)
        {
            // ۱. مشخص کردن Claims (ادعاها یا همان اطلاعات کاربر)
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

            // ۲. خواندن کلید سری از appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            // ۳. ساخت اعتبارنامه‌های امضا (Signing Credentials)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // ۴. ساخت ساختار توکن
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1), // توکن تا ۱ روز معتبر است
                signingCredentials: creds
            );

            // ۵. تبدیل توکن به رشته (String)
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



    }
}
