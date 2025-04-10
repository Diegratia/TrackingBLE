using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingBle.Models.Dto.MstBuildingDtos;
using TrackingBle.Services.Interfaces;
using System.Linq;

namespace TrackingBle.src._10MstBuilding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstBuildingController : ControllerBase
    {
        private readonly IMstBuildingService _service;

        public MstBuildingController(IMstBuildingService service)
        {
            _service = service;
        }

        // GET: api/MstBuilding
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var buildings = await _service.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    msg = "Buildings retrieved successfully",
                    collection = new { data = buildings },
                    code = 200
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

        // GET: api/MstBuilding/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var building = await _service.GetByIdAsync(id);
                if (building == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        msg = "Building not found",
                        collection = new { data = (object)null },
                        code = 404
                    });
                }
                return Ok(new
                {
                    success = true,
                    msg = "Building retrieved successfully",
                    collection = new { data = building },
                    code = 200
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

        // POST: api/MstBuilding
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MstBuildingCreateDto dto)
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
                var createdBuilding = await _service.CreateAsync(dto);
                return StatusCode(201, new
                {
                    success = true,
                    msg = "Building created successfully",
                    collection = new { data = createdBuilding },
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

        // PUT: api/MstBuilding/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MstBuildingUpdateDto dto)
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
                await _service.UpdateAsync(id, dto); // No var assignment
                return Ok(new
                {
                    success = true,
                    msg = "Integration updated successfully",
                    collection = new { data = (object)null },
                    code = 204
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

        // DELETE: api/MstBuilding/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new
                {
                    success = true,
                    msg = "Building deleted successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                    {
                        success = false,
                        msg = "Building not found",
                        collection = new { data = (object)null },
                        code = 404
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