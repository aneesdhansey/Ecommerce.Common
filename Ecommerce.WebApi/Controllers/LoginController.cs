using Ecommerce.WebApi.Data;
using Ecommerce.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly EcomDbContext _db;
        public LoginController(EcomDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == dto.UserName);

            if (user == null)
            {
               return BadRequest(new { message = "User not found" });
            }

            if(user.Password != dto.Password)
            {
                return BadRequest(new { message = "Invalid password" });
            }

            return Ok(new { success = true });
        }
    }
}
