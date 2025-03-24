using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingBle.src._16MstMember.Models.Dto.MstMemberDtos;

namespace TrackingBle.src._16MstMember.Services
{
    public interface IMstMemberService
    {
        Task<IEnumerable<MstMemberDto>> GetAllMembersAsync();
        Task<MstMemberDto> GetMemberByIdAsync(Guid id);
        Task<MstMemberDto> CreateMemberAsync(MstMemberCreateDto createDto);
        Task<MstMemberDto> UpdateMemberAsync(Guid id, MstMemberUpdateDto updateDto);
        Task DeleteMemberAsync(Guid id);
    }
}