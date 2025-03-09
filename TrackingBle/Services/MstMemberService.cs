using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackingBle.Data;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstMemberDto;

namespace TrackingBle.Services
{
    public class MstMemberService : IMstMemberService
    {
        private readonly TrackingBleDbContext _context;
        private readonly IMapper _mapper;

        public MstMemberService(TrackingBleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MstMemberDto>> GetAllMembersAsync()
        {
            var members = await _context.MstMembers.ToListAsync(); 
            return _mapper.Map<IEnumerable<MstMemberDto>>(members);
        }

        public async Task<MstMemberDto> GetMemberByIdAsync(Guid id)
        {
            var member = await _context.MstMembers.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<MstMemberDto>(member);
        }

        public async Task<MstMemberDto> CreateMemberAsync(MstMemberCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var member = _mapper.Map<MstMember>(dto);
            member.Id = Guid.NewGuid();
            member.Status = 1; 
            member.CreatedBy = ""; 
            member.CreatedAt = DateTime.UtcNow;
            member.UpdatedBy = ""; // bisa ganti dgn logic auth
            member.UpdatedAt = DateTime.UtcNow;

            _context.MstMembers.Add(member);
            await _context.SaveChangesAsync();

            return _mapper.Map<MstMemberDto>(member);
        }

        public async Task UpdateMemberAsync(Guid id, MstMemberUpdateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var member = await _context.MstMembers.FindAsync(id);
            if (member == null || member.Status == 0) // 0 = Deleted
            {
                throw new KeyNotFoundException($"Member with ID {id} not found or has been deleted.");
            }

            _mapper.Map(dto, member);
            member.UpdatedBy = ""; 
            member.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberAsync(Guid id)
        {
            var member = await _context.MstMembers.FindAsync(id);
            if (member == null || member.Status == 0) 
            {
                throw new KeyNotFoundException($"Member with ID {id} not found or already deleted.");
            }

            member.Status = 0; 
            member.UpdatedBy = ""; 
            member.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}