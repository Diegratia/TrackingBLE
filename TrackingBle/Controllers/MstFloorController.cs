using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingBle.Models.Dto.MstFloorDtos;
using TrackingBle.Services;
using System.Linq;

namespace TrackingBle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstFloorController : ControllerBase
    {
        private readonly IMstFloorService _mstFloorService;

        public MstFloorController(IMstFloorService mstFloorService)
        {
            _mstFloorService = mstFloorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var floors = await _mstFloorService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    msg = "Floors retrieved successfully",
                    collection = new { data = floors },
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var floor = await _mstFloorService.GetByIdAsync(id);
                if (floor == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        msg = "Floor not found",
                        collection = new { data = (object)null },
                        code = 404
                    });
                }
                return Ok(new
                {
                    success = true,
                    msg = "Floor retrieved successfully",
                    collection = new { data = floor },
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MstFloorCreateDto mstFloorDto)
        {   
            if (!ModelState.IsValid || (mstFloorDto.FloorImage != null && mstFloorDto.FloorImage.Length == 0))
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
                var createdFloor = await _mstFloorService.CreateAsync(mstFloorDto);
                return StatusCode(201, new
                {
                    success = true,
                    msg = "Floor created successfully",
                    collection = new { data = createdFloor },
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] MstFloorUpdateDto mstFloorDto)
        {
           if (!ModelState.IsValid || (mstFloorDto.FloorImage != null && mstFloorDto.FloorImage.Length == 0))
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
                var updatedFloor = await _mstFloorService.UpdateAsync(id, mstFloorDto);
                return Ok(new
                {
                    success = true,
                    msg = "Floor updated successfully",
                    collection = new { data = updatedFloor },
                    code = 200 
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Floor not found",
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mstFloorService.DeleteAsync(id);
                return Ok(new
                {
                    success = true,
                    msg = "Floor deleted successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Floor not found",
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