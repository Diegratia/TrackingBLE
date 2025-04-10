using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstDepartmentDtos;
using TrackingBle.Models.Dto.MstApplicationDtos;

namespace TrackingBle.MappingProfiles
{
    public class MstDepartmentProfile : Profile
    {
        public MstDepartmentProfile()
        {
            CreateMap<MstDepartment, MstDepartmentDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
                // .ForMember(dest => dest.Application, opt => opt.MapFrom(src => src.Application));
            CreateMap<MstDepartmentCreateDto, MstDepartment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<MstDepartmentUpdateDto, MstDepartment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            // CreateMap<MstApplication, MstApplicationDto>();
        }
    }
}