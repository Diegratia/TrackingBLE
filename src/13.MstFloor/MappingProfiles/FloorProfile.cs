using AutoMapper;
using TrackingBle.src._13MstFloor.Models.Domain;
using TrackingBle.src._13MstFloor.Models.Dto.MstFloorDtos;

namespace TrackingBle.src._13MstFloor.MappingProfiles
{
    public class MstFloorProfile : Profile
    {
        public MstFloorProfile()
        {
            // Mapping dari MstFloor ke MstFloorDto
            CreateMap<MstFloor, MstFloorDto>()
                .ForMember(dest => dest.Generate, opt => opt.MapFrom(src => src.Generate))
                .ForMember(dest => dest.FloorImage, opt => opt.MapFrom(src => src.FloorImage ?? "")); // Default ke "" jika null

            // Mapping dari MstFloorCreateDto ke MstFloor
            CreateMap<MstFloorCreateDto, MstFloor>()
                .ForMember(dest => dest.FloorImage, opt => opt.Ignore()); // Diisi manual di service setelah upload

            // Mapping dari MstFloorUpdateDto ke MstFloor
            // Catatan: Anda belum memberikan MstFloorUpdateDto, jadi saya asumsikan mirip dengan CreateDto
            CreateMap<MstFloorCreateDto, MstFloor>()
                .ForMember(dest => dest.FloorImage, opt => opt.Ignore()) // Diisi manual di service setelah upload
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Hanya update field yang tidak null
        }
    }
}