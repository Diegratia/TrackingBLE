using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingBle.Models.Dto.MstBrandDto;
using TrackingBle.Services;
using System.Linq;

namespace TrackingBle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MstBrandController : ControllerBase
    {
        private readonly IMstBrandService _mstBrandService;

        public MstBrandController(IMstBrandService mstBrandService)
        {
            _mstBrandService = mstBrandService;
        }

        // GET: api/MstBrand
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var brands = await _mstBrandService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    msg = "Brands retrieved successfully",
                    collection = new { data = brands },
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

        // GET: api/MstBrand/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var brand = await _mstBrandService.GetByIdAsync(id);
                if (brand == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        msg = "Brand not found",
                        collection = new { data = (object)null },
                        code = 404
                    });
                }
                return Ok(new
                {
                    success = true,
                    msg = "Brand retrieved successfully",
                    collection = new { data = brand },
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

        // POST: api/MstBrand
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MstBrandCreateDto mstBrandDto)
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
                var createdBrand = await _mstBrandService.CreateAsync(mstBrandDto);
                return StatusCode(201, new
                {
                    success = true,
                    msg = "Brand created successfully",
                    collection = new { data = createdBrand },
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

        // PUT: api/MstBrand/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MstBrandUpdateDto mstBrandDto)
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
                await _mstBrandService.UpdateAsync(id, mstBrandDto);
                return Ok(new
                {
                    success = true,
                    msg = "Brand updated successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Brand not found",
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

        // DELETE: api/MstBrand/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mstBrandService.DeleteAsync(id);
                return Ok(new
                {
                    success = true,
                    msg = "Brand deleted successfully",
                    collection = new { data = (object)null },
                    code = 204
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    msg = "Brand not found",
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