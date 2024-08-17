using AutoMapper;
using CarDealership.Models;

namespace CarDealership.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(dest => dest.DealerShipName, opt => opt.MapFrom(src => src.Dealership != null ? src.Dealership.Name : ""));
        }
    }
}
