using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TF_IOT2025_DemoJwt_API.Entities;
using TF_IOT2025_DemoJwt_API.Entities.Contexts;
using TF_IOT2025_DemoJwt_API.Utils.Extensions;

namespace TF_IOT2025_DemoJwt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {

        private readonly MyDbContext _context;

        public HouseController(MyDbContext context)
        {
            _context = context;
        }

        [Authorize("auth")]
        [HttpGet]
        public IActionResult GetOwnedHouses()
        {
            int userId = User.GetId();

            List<House> houses = _context.Houses
                .Where(h => h.Users.Any(u => u.Id == userId))
                .ToList();

            return Ok(houses);
        }
    }
}
