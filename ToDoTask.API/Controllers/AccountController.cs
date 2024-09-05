using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoTask.Application.DTOs;
using ToDoTask.Application.Interfaces;

namespace ToDoTask.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = await _userService.RegisterUserAsync(registerUserDto);

            if (result)
                return Ok("User registered successfully");

            return BadRequest("User registration failed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userService.LoginUserAsync(loginUserDto);

            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { Token = token });
        }
    }
}
