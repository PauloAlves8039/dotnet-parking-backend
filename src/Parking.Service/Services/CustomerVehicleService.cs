using AutoMapper;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Interfaces.Services;
using Parking.Model.Models;

namespace Parking.Service.Services;

public class CustomerVehicleService : Service<CustomerVehicle, CustomerVehicleDTO>, ICustomerVehicleService
{
    public CustomerVehicleService(IRepository<CustomerVehicle> repository, IMapper mapper) : base(repository, mapper) { }
}
