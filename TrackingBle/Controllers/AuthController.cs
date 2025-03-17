using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.AuthDtos;
using TrackingBle.Services;

namespace TrackingBle.Controllers
{
    [Route("api/[controller]")]
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
                return BadRequest(new { success = false, msg = "Invalid data", collection = new { data = (object)null }, code = 400 });

            try
            {
                var response = await _authService.LoginAsync(dto);
                return Ok(new { success = true, msg = "Login successful", collection = new { data = response }, code = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, msg = ex.Message, collection = new { data = (object)null }, code = 400 });
            }
        }
    }
}