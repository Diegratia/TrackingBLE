using AutoMapper;
using TrackingBle.src._8MstBleReader.Models.Domain;
using TrackingBle.src._8MstBleReader.Models.Dto.MstBleReaderDtos;

namespace TrackingBle.src._8MstBleReader.MappingProfiles
{
    public class MstBleReaderProfile : Profile
    {
        public MstBleReaderProfile()
        {
            CreateMap<MstBleReader, MstBleReaderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
            CreateMap<MstBleReaderCreateDto, MstBleReader>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<MstBleReaderUpdateDto, MstBleReader>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Generate, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}