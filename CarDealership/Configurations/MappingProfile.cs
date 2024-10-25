using AutoMapper;
using CarDealership.Models;

namespace CarDealership.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(d => d.UserCompleteName, opt => opt.MapFrom(src => src.User!.Name + " " + src.User.Lastname))
                .ForMember(d => d.AgentCompleteName, opt => opt.MapFrom(src => src.Agent!.Name + " " + src.Agent.Lastname))
                .ForMember(d => d.HouseLocation, opt => opt.MapFrom(src => src.House!.Location));
        }
    }
}
