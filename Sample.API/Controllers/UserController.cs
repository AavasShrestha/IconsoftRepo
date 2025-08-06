using CBS.Data.DTO;
using CBS.Data.TenantDB;
using CBS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Sample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/user
        [HttpPost]
        public IActionResult CreateUser(
            [FromHeader(Name = "User-ID")] int userId,
            [FromBody] UserDetail userDetail)
        {
            if (userDetail == null)
                return BadRequest("Invalid user data.");

            var createdUser = _userService.CreateUser(userId, userDetail);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDetail userDetail)
        {
            if (userDetail == null || id <= 0)
                return BadRequest("Invalid request.");

            var updatedUser = _userService.UpdateUser(id, userDetail);
            if (updatedUser == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(updatedUser);
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            if (!result)
                return NotFound($"User with ID {id} not found.");

            return NoContent();
        }

        // GET: api/user
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound($"User with ID {id} not found.");

            return Ok(user);
        }

        //// POST: api/user/login
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginRequest request)
        //{
        //    if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        //        return BadRequest("Username and password are required.");

        //    var user = _userService.GetLoggedInUserDetail(request.Username, request.Password);
        //    if (user == null)
        //        return Unauthorized("Invalid credentials.");

        //    return Ok(user);
        //}

        //// POST: api/user/authenticate
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        //{
        //    var isValid = _userService.UserAuthentication(request.UserId, request.Password);
        //    return Ok(new { IsValid = isValid });
        //}


        //public class LoginRequest
        //{
        //    public string Username { get; set; }
        //    public string Password { get; set; }
        //}

        //public class AuthenticationRequest
        //{
        //    public int UserId { get; set; }
        //    public string Password { get; set; }
        //}
    }
}
