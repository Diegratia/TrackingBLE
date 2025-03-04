using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingBle.Models.Dto.MstAccessControlDto;
using TrackingBle.Services;
using System.Linq;

namespace TrackingBle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstAccessControlController : ControllerBase
    {
        private readonly IMstAccessControlService _mstAccessControlService;

        public MstAccessControlController(IMstAccessControlService mstAccessControlService)
        {
            _mstAccessControlService = mstAccessControlService;
        }

        // GET: api/MstAccessControl
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var accessControls = await _mstAccessControlService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    msg = "Access Controls retrieved successfully",
                    collection = new { data = accessControls },
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

        // GET: api/MstAccessControl/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var accessControl = await _mstAccessControlService.GetByIdAsync(id);
                if (accessControl == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        msg = "Access Control not found",
                        collection = new { data = (object)null },
                        code = 404
                    });
                }
                return Ok(new
                {
                    success = true,
                    msg = "Access Control retrieved successfully",
                    collection = new { data = accessControl },
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

        // POST: api/MstAccessControl
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MstAccessControlCreateDto mstAccessControlDto)
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
                var createdAccessControl = await _mstAccessControlService.CreateAsync(mstAccessControlDto);
                return StatusCode(201, new
                {
                    success = true,
                    msg = "Access Control created successfully",
                    collection = new { data = createdAccessControl },
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

        // PUT: api/MstAccessControl/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MstAccessControlUpdateDto mstAccessControlDto)
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
                await _mstAccessControlService.UpdateAsync(id, mstAccessControlDto);
                return Ok(new
                {
                    success = true,
                    msg = "Access Control updated successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Access Control not found",
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

        // DELETE: api/MstAccessControl/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mstAccessControlService.DeleteAsync(id);
                return Ok(new
                {
                    success = true,
                    msg = "Access Control deleted successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Access Control not found",
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