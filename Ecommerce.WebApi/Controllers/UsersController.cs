using Ecommerce.WebApi.Data;
using Ecommerce.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EcomDbContext _db;
        public UsersController(EcomDbContext context)
        {
            _db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
        {
            var newUser = new User
            {
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth
            };

            _db.Users.Add(newUser);

            await _db.SaveChangesAsync();

            return Ok(newUser.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _db.Users.ToListAsync(); // SELECT * FROM Users

            return Ok(users);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            //// SELECT TOP 1 * FROM Users Where Id = userId
            //var user = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            //var user2 = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var user3 = await _db.Users.FindAsync(userId);


            //SELECT TOP 1 FirstName, LastName From Users Where Id = 3
            var userColumns = await _db.Users.Where(x => x.Id == userId).Select(x => new { x.FirstName, x.LastName }).FirstOrDefaultAsync();

            if (user3 is null)
            {
                return NotFound();
            }

            return Ok(userColumns);
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto dto)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound($"User with Id {userId} not found");
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Password = dto.Password;

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound($"User with Id {userId} not found");
            }

            _db.Users.Remove(user);

            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
