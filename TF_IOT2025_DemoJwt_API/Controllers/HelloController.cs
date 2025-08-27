using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TF_IOT2025_DemoJwt_API.Enums;
using TF_IOT2025_DemoJwt_API.Utils.Extensions;

namespace TF_IOT2025_DemoJwt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {

        [Authorize("Auth")]
        [HttpGet]
        public IActionResult SayHello()
        {
            int id = User.GetId();
            string username = User.GetUserName();
            UserRole role = User.GetRole(); 
            return Ok(new { Content = $"Hello {username}" });
        }
    }
}
