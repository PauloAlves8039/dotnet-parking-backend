using AutoMapper;
using Parking.Model.DTOs;
using Parking.Model.Models;

namespace Parking.IoC.Mappings;

public class ParkingProfile : Profile
{
    public ParkingProfile()
    {
        CreateMap<Customer, CustomerDTO>().ReverseMap();
        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<Vehicle, VehicleDTO>().ReverseMap();
        CreateMap<CustomerVehicle, CustomerVehicleDTO>().ReverseMap();
        CreateMap<Stay, StayDTO>().ReverseMap();
    }
}
