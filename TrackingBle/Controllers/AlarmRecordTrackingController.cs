using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrackingBle.Models.Dto.AlarmRecordTrackingDtos;
using TrackingBle.Services;

namespace TrackingBle.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alarms = await _service.GetAllAsync();
            return Ok(alarms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var alarm = await _service.GetByIdAsync(id);
            if (alarm == null) return NotFound();
            return Ok(alarm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlarmRecordTrackingCreateDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var alarm = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = alarm.Id }, alarm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AlarmRecordTrackingUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}