using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrackingBle.src._1Auth.Models.Dto.AuthDtos;
using TrackingBle.src._1Auth.Data;
using TrackingBle.src._1Auth.Models.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace TrackingBle.src._1Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly AuthDbContext _context;
        private readonly IMapper _mapper;

        public UserGroupController(AuthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _context.UserGroups.ToListAsync();
            var groupDtos = _mapper.Map<List<UserGroupDto>>(groups);
            return Ok(groupDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var group = await _context.UserGroups.FindAsync(id);
            if (group == null)
                return NotFound();
            var groupDto = _mapper.Map<UserGroupDto>(group);
            return Ok(groupDto);
        }
    }
}