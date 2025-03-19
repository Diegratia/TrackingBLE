using AutoMapper;
using TrackingBle.src._4FloorplanMaskedArea.Models.Domain;
using TrackingBle.src._4FloorplanMaskedArea.Models.Dto.FloorplanMaskedAreaDtos;

namespace TrackingBle.src._4FloorplanMaskedArea.MappingProfiles
{
    public class FloorplanMaskedAreaProfile : Profile
    {
        public FloorplanMaskedAreaProfile()
        {
            // Mapping dari FloorplanMaskedArea ke FloorplanMaskedAreaDto
            CreateMap<FloorplanMaskedArea, FloorplanMaskedAreaDto>()
                .ForMember(dest => dest.Generate, opt => opt.Ignore()) // Biasanya diisi manual atau dari logika lain
                .ForMember(dest => dest.Floor, opt => opt.Ignore()) // Diisi manual via service
                .ForMember(dest => dest.Floorplan, opt => opt.Ignore()); // Diisi manual via service

            // Mapping dari FloorplanMaskedAreaCreateDto ke FloorplanMaskedArea
            CreateMap<FloorplanMaskedAreaCreateDto, FloorplanMaskedArea>();

            // Mapping dari FloorplanMaskedAreaUpdateDto ke FloorplanMaskedArea
            // Catatan: Anda belum memberikan FloorplanMaskedAreaUpdateDto, jadi saya asumsikan mirip dengan CreateDto
            CreateMap<FloorplanMaskedAreaCreateDto, FloorplanMaskedArea>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Hanya update field yang tidak null
        }
    }
}