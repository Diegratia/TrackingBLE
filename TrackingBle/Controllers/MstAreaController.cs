using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingBle.Models.Dto;
using TrackingBle.Services;

namespace TrackingBle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstAreaController : ControllerBase
    {
        private readonly IMstAreaService _mstAreaService;

        public MstAreaController(IMstAreaService mstAreaService)
        {
            _mstAreaService = mstAreaService;
        }

        // GET: api/MstArea
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var areas = await _mstAreaService.GetAllAsync();
                return Ok(areas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MstArea/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) // Ubah dari string ke Guid
        {
            try
            {
                var area = await _mstAreaService.GetByIdAsync(id);
                if (area == null) return NotFound();
                return Ok(area);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/MstArea
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MstAreaDto mstAreaDto) // Gunakan DTO
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                Console.WriteLine("Validation errors: " + string.Join(", ", errors));
                return BadRequest(ModelState);
            }

            try
            {
                var createdArea = await _mstAreaService.CreateAsync(mstAreaDto);
                return CreatedAtAction(nameof(GetById), new { id = createdArea.Id }, createdArea);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/MstArea/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MstAreaDto mstAreaDto) // Ubah dari string ke Guid
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage);
                Console.WriteLine("Validation errors: " + string.Join(", ", errors));
                return BadRequest(ModelState);
            }

            if (id != mstAreaDto.Id)
            {
                return BadRequest("Id in URL must match Id in body");
            }

            try
            {
                await _mstAreaService.UpdateAsync(id, mstAreaDto);
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

        // DELETE: api/MstArea/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) // Ubah dari string ke Guid
        {
            try
            {
                await _mstAreaService.DeleteAsync(id);
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