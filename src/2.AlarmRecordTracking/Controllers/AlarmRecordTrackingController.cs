using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrackingBle.src._2AlarmRecordTracking.Models.Dto.AlarmRecordTrackingDtos;
using TrackingBle.src._2AlarmRecordTracking.Services;
using System.Linq;

namespace TrackingBle.src._2AlarmRecordTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmRecordTrackingController : ControllerBase
    {
        private readonly IAlarmRecordTrackingService _service;

        public AlarmRecordTrackingController(IAlarmRecordTrackingService service)
        {
            _service = service;
        }

        // GET: api/AlarmRecordTracking
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var alarms = await _service.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    msg = "Alarm records retrieved successfully",
                    collection = new { data = alarms },
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

        // GET: api/AlarmRecordTracking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var alarm = await _service.GetByIdAsync(id);
                if (alarm == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        msg = "Alarm record not found",
                        collection = new { data = (object)null },
                        code = 404
                    });
                }
                return Ok(new
                {
                    success = true,
                    msg = "Alarm record retrieved successfully",
                    collection = new { data = alarm },
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

        // POST: api/AlarmRecordTracking
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlarmRecordTrackingCreateDto createDto)
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
                var alarm = await _service.CreateAsync(createDto);
                return StatusCode(201, new
                {
                    success = true,
                    msg = "Alarm record created successfully",
                    collection = new { data = alarm },
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

        // PUT: api/AlarmRecordTracking/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AlarmRecordTrackingUpdateDto updateDto)
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
                await _service.UpdateAsync(id, updateDto);
                return Ok(new
                {
                    success = true,
                    msg = "Alarm record updated successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Alarm record not found",
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

        // DELETE: api/AlarmRecordTracking/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new
                {
                    success = true,
                    msg = "Alarm record deleted successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Alarm record not found",
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