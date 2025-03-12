using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingBle.Models.Dto.MstBleReaderDtos;
using TrackingBle.Services;
using System.Linq;

namespace TrackingBle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstBleReaderController : ControllerBase
    {
        private readonly IMstBleReaderService _mstBleReaderService;

        public MstBleReaderController(IMstBleReaderService mstBleReaderService)
        {
            _mstBleReaderService = mstBleReaderService;
        }

        // GET: api/MstBleReader
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var bleReaders = await _mstBleReaderService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    msg = "BLE Readers retrieved successfully",
                    collection = new { data = bleReaders },
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

        // GET: api/MstBleReader/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var bleReader = await _mstBleReaderService.GetByIdAsync(id);
                if (bleReader == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        msg = "BLE Reader not found",
                        collection = new { data = (object)null },
                        code = 404
                    });
                }
                return Ok(new
                {
                    success = true,
                    msg = "BLE Reader retrieved successfully",
                    collection = new { data = bleReader },
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

        // POST: api/MstBleReader
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MstBleReaderCreateDto mstBleReaderDto)
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
                var createdBleReader = await _mstBleReaderService.CreateAsync(mstBleReaderDto);
                return StatusCode(201, new
                {
                    success = true,
                    msg = "BLE Reader created successfully",
                    collection = new { data = createdBleReader },
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

        // PUT: api/MstBleReader/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MstBleReaderUpdateDto mstBleReaderDto)
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
                await _mstBleReaderService.UpdateAsync(id, mstBleReaderDto);
                return Ok(new
                {
                    success = true,
                    msg = "BLE Reader updated successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "BLE Reader not found",
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

        // DELETE: api/MstBleReader/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mstBleReaderService.DeleteAsync(id);
                return Ok(new
                {
                    success = true,
                    msg = "BLE Reader deleted successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "BLE Reader not found",
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