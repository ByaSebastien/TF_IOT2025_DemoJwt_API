using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TF_IOT2025_DemoJwt_API.Dtos;
using TF_IOT2025_DemoJwt_API.Entities;
using TF_IOT2025_DemoJwt_API.Entities.Contexts;
using TF_IOT2025_DemoJwt_API.Enums;
using TF_IOT2025_DemoJwt_API.Mappers;
using TF_IOT2025_DemoJwt_API.Utils;

namespace TF_IOT2025_DemoJwt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly MyDbContext _context;
        private readonly JwtUtils _jwtUtils;

        public AuthController(MyDbContext context, JwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterFormDTO form)
        {
            if (_context.Users.Any(u => u.Username == form.Username))
            {
                return BadRequest(new { Content = $"User with username {form.Username} already exist" });
            }

            User user = form.ToUser();
            user.Password = Argon2.Hash(form.Password);
            user.Role = UserRole.USER;

            _context.Users.Add(user);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult<UserTokenDTO> Login([FromBody] LoginFormDTO form)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Username == form.Username);

            if (user == null)
            {
                return BadRequest(new { Content = $"User with username {form.Username} doesn't exist" });
            }

            if (!Argon2.Verify(user.Password, form.Password))
            {
                return BadRequest(new { Content = "Wrong password" });
            }

            UserDTO dto = user.ToUserDTO();
            string token = _jwtUtils.GenerateToken(user);

            UserTokenDTO result = new UserTokenDTO()
            {
                User = dto,
                Token = token
            };

            return Ok(result);
        }
    }
}
