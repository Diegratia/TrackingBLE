using AutoMapper;
using TrackingBle.src._7MstApplication.Models.Domain;
using TrackingBle.src._7MstApplication.Models.Dto.MstApplicationDtos;

namespace TrackingBle.src._7MstApplication.MappingProfiles
{
    public class MstApplicationProfile : Profile
    {
             public MstApplicationProfile()
        {
            CreateMap<MstApplication, MstApplicationDto>()
            .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => src.ApplicationStatus));

            CreateMap<MstApplicationCreateDto, MstApplication>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationRegistered, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<MstApplicationUpdateDto, MstApplication>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationRegistered, opt => opt.Ignore());

        }
    }
}