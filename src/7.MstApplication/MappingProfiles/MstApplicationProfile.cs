using AutoMapper;
using TrackingBle.src._7MstApplication.Models.Domain;
using TrackingBle.src._7MstApplication.Models.Dto.MstApplicationDtos;

namespace TrackingBle.src._7MstApplication.MappingProfiles
{
    public class MstApplicationProfile : Profile
    {
/*************  ✨ Codeium Command ⭐  *************/
        /// <summary>
        /// Maps the MstApplicationCreateDto, MstApplicationUpdateDto and MstApplication to the respective dtos.
        /// </summary>
/******  e935153a-a1d1-4530-a38b-b53900c025ff  *******/        public MstApplicationProfile()
        {
            CreateMap<MstApplicationCreateDto, MstApplication>();
                // .ForMember(dest => dest.OrganizationType, opt => opt.MapFrom(src => Enum.Parse<OrganizationType>(src.OrganizationType)))
                // .ForMember(dest => dest.ApplicationType, opt => opt.MapFrom(src => Enum.Parse<ApplicationType>(src.ApplicationType)))
                // .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => Enum.Parse<LicenseType>(src.LicenseType)));

            CreateMap<MstApplicationUpdateDto, MstApplication>();
                // .ForMember(dest => dest.OrganizationType, opt => opt.MapFrom(src => Enum.Parse<OrganizationType>(src.OrganizationType)))
                // .ForMember(dest => dest.ApplicationType, opt => opt.MapFrom(src => Enum.Parse<ApplicationType>(src.ApplicationType)))
                // .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => Enum.Parse<LicenseType>(src.LicenseType)));

            CreateMap<MstApplication, MstApplicationDto>();
                // .ForMember(dest => dest.OrganizationType, opt => opt.MapFrom(src => src.OrganizationType.ToString()))
                // .ForMember(dest => dest.ApplicationType, opt => opt.MapFrom(src => src.ApplicationType.ToString()))
                // .ForMember(dest => dest.LicenseType, opt => opt.MapFrom(src => src.LicenseType.ToString()));
        }
    }
}