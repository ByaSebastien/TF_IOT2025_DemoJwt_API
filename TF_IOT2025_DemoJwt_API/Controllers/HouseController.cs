using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TF_IOT2025_DemoJwt_API.Dtos;
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

        [Authorize("auth")]
        [HttpPost]
        public IActionResult AddHouse([FromBody] HouseAddDto dto)
        {
            // je cherche dans la db si la maison existe dejà
            House? house = _context.Houses.FirstOrDefault(h => h.Name == dto.Name);
            if(house == null)
            {
                // si elle n'existe pas je l'enregistre dans la DB
                house = _context.Add(
                    new House { Name = dto.Name }
                ).Entity;
                _context.SaveChanges();
            }

            // je cherche l'utilisateur connecté et les maisons
            // qui lui sont attribuées 
            User user = _context.Users
                .Include(u => u.Houses)
                .FirstOrDefault(u => u.Id == User.GetId())!;

            // j'ajoute à l'uilisateur la maison
            user.Houses.Add(house);
            _context.SaveChanges();
            return Created();
        }
    }
}
