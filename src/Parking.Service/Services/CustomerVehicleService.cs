using AutoMapper;
using Parking.Model.Interfaces;
using Parking.Model.Models;
using Parking.Service.DTOs;
using Parking.Service.Interfaces.Business;

namespace Parking.Service.Services;

public class CustomerVehicleService : Service<CustomerVehicle, CustomerVehicleDTO>, ICustomerVehicleService
{
    public CustomerVehicleService(IRepository<CustomerVehicle> repository, IMapper mapper) : base(repository, mapper) { }
}
