using AutoMapper;
using Project.Application.DTOs;
using Project.Domain.Entities;

namespace Project.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<GuruProfile, GuruProfileDto>().ReverseMap();
            CreateMap<CustomerProfile, CustomerProfileDto>().ReverseMap();
            CreateMap<AvailabilitySlot, AvailabilitySlotDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
