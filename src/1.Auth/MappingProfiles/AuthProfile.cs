using AutoMapper;
using TrackingBle.src._1Auth.Models.Domain;
using TrackingBle.src._1Auth.Models.Dto.AuthDtos;

namespace TrackingBle.src._1Auth.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthResponseDto>()
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => src.IsEmailConfirmation == 1))
                .ForMember(dest => dest.StatusActive, opt => opt.MapFrom(src => src.StatusActive.ToString()));

            CreateMap<UserGroup, UserGroupDto>()
                .ForMember(dest => dest.LevelPriority, opt => opt.MapFrom(src => src.LevelPriority.ToString()));
        }
    }
}