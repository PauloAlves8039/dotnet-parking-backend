using AutoMapper;
using Parking.Model.Models;
using Parking.Service.DTOs;

namespace Parking.Service.Mappings;

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
