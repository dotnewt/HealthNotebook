using HealthNotebook.DataService.Data;
using HealthNotebook.Entities.DbSet;
using HealthNotebook.Entities.DTOs.Incoming;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthNotebook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // Get all users
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.Where(x => x.Status == 1).ToList();

            return Ok(users);
        }

        // Post
        [HttpPost]
        [Route("AddNewUser")]
        public IActionResult AddUser(UserDTO user)
        {
            User _user = new User();
            _user.FirstName= user.FirstName;
            _user.LastName= user.LastName;
            _user.Email= user.Email;
            _user.DateOfBirth = Convert.ToDateTime(user.DateOfBirth);
            _user.Phone = user.Phone;
            _user.Country = user.Country;

            _context.Users.Add(_user);
            _context.SaveChanges();

            return Ok();
        }

        // Get user by id
        public IActionResult GetUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            return Ok(user);
        }
    }
}
