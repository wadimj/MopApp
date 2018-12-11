using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Models.User;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController(MopContext context)
        {
            _userRepository = new UserRepository(context);
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(User userParam)
        {
            var user = await _userRepository.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        
        // GET api/user
        [HttpGet]
        public IEnumerable<object> Get(int skip = 0, int limit = 100)
        {
            var identity = (ClaimsIdentity)User.Identity;
            
            var Roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value).ToArray();
            
            limit = limit <= 100 ? limit : 100;
            return _userRepository
                .GetUsers()
                .Skip(skip)
                .Select(x => new
                {
                    Id = x.Id,
                    Forename = x.Forename,
                    Surname = x.Surname,
                    Email = x.Email,
                    Roles = x.UserRoles.Select(y => new
                    {
                        Id = y.Role.Id,
                        Name = y.Role.Name
                    })
                })
                .Take(limit);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUser")]
        public User Get(int id)
        {
            return _userRepository.GetUserById(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(User user)
        {
            _userRepository.InsertUser(user);
            _userRepository.Save();
            
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }
    }
}