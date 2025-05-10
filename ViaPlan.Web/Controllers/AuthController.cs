using Microsoft.AspNetCore.Mvc;
using ViaPlan.Services;
using ViaPlan.Services.DTO;

namespace ViaPlan.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthServices _authService;

        public AuthController(AuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var result = await _authService.RegisterUserAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}