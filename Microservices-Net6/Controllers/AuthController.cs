using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microservices_Net6.DTOs;
using Microservices_Net6.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Microservices_Net6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register([FromBody] RegisterRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Registration attempt for user: {Email}", request.Email);

            var result = await _userService.RegisterAsync(request);
            if (result == null)
                return BadRequest("Email already in use");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Login attempt for user: {Email}", request.Email);

            var result = await _userService.LoginAsync(request);
            if (result == null)
                return Unauthorized("Invalid email or password");

            return Ok(result);
        }
    }
}