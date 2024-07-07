using AutoMapper;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Interfaces.Services;
using Parking.Model.Models;

namespace Parking.Service.Services.Implementations;

public class AddressService : Service<Address, AddressDTO>, IAddressService
{
    public AddressService(IRepository<Address> repository, IMapper mapper) : base(repository, mapper) { }
}
