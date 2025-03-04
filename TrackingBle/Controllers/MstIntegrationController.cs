using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingBle.Models.Dto;
using TrackingBle.Services;

namespace TrackingBle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstIntegrationController : ControllerBase
    {
        private readonly IMstIntegrationService _mstIntegrationService;

        public MstIntegrationController(IMstIntegrationService mstIntegrationService)
        {
            _mstIntegrationService = mstIntegrationService;
        }

        // GET: api/MstIntegration
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var integrations = await _mstIntegrationService.GetAllAsync();
                return Ok(integrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MstIntegration/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var integration = await _mstIntegrationService.GetByIdAsync(id);
                if (integration == null) return NotFound();
                return Ok(integration);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/MstIntegration
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MstIntegrationDto mstIntegrationDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                Console.WriteLine("Validation errors: " + string.Join(", ", errors));
                return BadRequest(ModelState);
            }

            try
            {
                var createdIntegration = await _mstIntegrationService.CreateAsync(mstIntegrationDto);
                return CreatedAtAction(nameof(GetById), new { id = createdIntegration.Id }, createdIntegration);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/MstIntegration/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MstIntegrationDto mstIntegrationDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                Console.WriteLine("Validation errors: " + string.Join(", ", errors));
                return BadRequest(ModelState);
            }

            if (id != mstIntegrationDto.Id)
            {
                return BadRequest("Id in URL must match Id in body");
            }

            try
            {
                await _mstIntegrationService.UpdateAsync(id, mstIntegrationDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/MstIntegration/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mstIntegrationService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}