using AutoMapper;
using TrackingBle.Models.Domain;
using TrackingBle.Models.Dto.MstBleReaderDto;

namespace TrackingBle.MappingProfiles
{
    public class MstBleReaderProfile : Profile
    {
        public MstBleReaderProfile()
        {
            CreateMap<MstBleReader, MstBleReaderDto>();
            CreateMap<MstBleReaderCreateDto, MstBleReader>();
            CreateMap<MstBleReaderUpdateDto, MstBleReader>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}