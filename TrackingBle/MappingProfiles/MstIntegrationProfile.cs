using AutoMapper;
using TrackingBle.Models.Dto.MstIntegrationDto;
using TrackingBle.Models.Domain;

namespace TrackingBle.MappingProfiles
{
    public class MstIntegrationProfile : Profile
    {
        public MstIntegrationProfile()
        {
            CreateMap<MstIntegration, MstIntegrationDto>();
            CreateMap<MstIntegrationCreateDto, MstIntegration>();
            CreateMap<MstIntegrationUpdateDto, MstIntegration>();
        }
    }
}