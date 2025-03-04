using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstApplicationDto;

namespace TrackingBle.MappingProfiles
{
    public class MstApplicationProfile : Profile
    {
        public MstApplicationProfile()
        {
            // Dari MstApplication ke MstApplicationDto (untuk GET)
            CreateMap<MstApplication, MstApplicationDto>();

            // Dari MstApplicationCreateDto ke MstApplication (untuk POST)
            CreateMap<MstApplicationCreateDto, MstApplication>();

            // Dari MstApplicationUpdateDto ke MstApplication (untuk PUT)
            CreateMap<MstApplicationUpdateDto, MstApplication>();
        }
    }
}