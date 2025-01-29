using EmployerPortal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployerPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(
            ILogger<UsersController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("welcome")]
        public async Task<IActionResult> Welcome([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest(new { Message = "Username is required." });
            
            try
            {
                var user = await _userService.GetUserByUsernameAsync(username);

                if (user == null)
                {
                    return NotFound(new { Message = "User not found." });
                }

                return Ok(new { Message = $"Hello, {user.Name}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, @$"Error retrieving user with username {username}");
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }
    }
}
