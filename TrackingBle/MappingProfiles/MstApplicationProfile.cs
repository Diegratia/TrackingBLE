using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstApplicationDto;

namespace TrackingBle.MappingProfiles
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



