using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrackingBle.src._1Auth.Models.Dto.AuthDtos;
using TrackingBle.src._1Auth.Services;

namespace TrackingBle.src._1Auth.Controllers
{
    [Route("/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                return BadRequest(new
                {
                    success = false,
                    msg = "Validation failed: " + string.Join(", ", errors),
                    collection = new { data = (object)null },
                    code = 400
                });
            }

            try
            {
                var response = await _authService.LoginAsync(dto);
                return Ok(new
                {
                    success = true,
                    msg = "Login successful",
                    collection = new { data = response },
                    code = 200
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    success = false,
                    msg = ex.Message ?? "Invalid credentials",
                    collection = new { data = (object)null },
                    code = 401
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    msg = $"Internal server error: {ex.Message}",
                    collection = new { data = (object)null },
                    code = 500
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                return BadRequest(new
                {
                    success = false,
                    msg = "Validation failed: " + string.Join(", ", errors),
                    collection = new { data = (object)null },
                    code = 400
                });
            }

            try
            {
                var response = await _authService.RegisterAsync(dto);
                return StatusCode(201, new
                {
                    success = true,
                    msg = "Registration successful",
                    collection = new { data = response },
                    code = 201
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    msg = $"Internal server error: {ex.Message}",
                    collection = new { data = (object)null },
                    code = 500
                });
            }
        }
    }
}