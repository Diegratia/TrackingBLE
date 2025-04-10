using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.AuthDtos;

namespace TrackingBle.src.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => src.IsEmailConfirmation == 1))
                .ForMember(dest => dest.StatusActive, opt => opt.MapFrom(src => src.StatusActive.ToString()));
        }
    }
}