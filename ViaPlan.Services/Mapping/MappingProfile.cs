using AutoMapper;
using ViaPlan.Entities;
using ViaPlan.Services.DTO;

namespace ViaPlan.Services.Mapping
{
    public class MappingProfile : Profile
    {
       public MappingProfile()
        {
            CreateMap<Trip, TripDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Trip, CreateTripDTO>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}