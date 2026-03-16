using AutoMapper;
using ProBook.Application.DTOs;
using ProBook.Domain.Entities;

namespace ProBook.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}