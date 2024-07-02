using AutoMapper;
using Parking.Model.Interfaces;
using Parking.Model.Models;
using Parking.Service.DTOs;
using Parking.Service.Interfaces.Business;

namespace Parking.Service.Services;

public class AddressService : Service<Address, AddressDTO>, IAddressService
{
    public AddressService(IRepository<Address> repository, IMapper mapper) : base(repository, mapper) { }
}
