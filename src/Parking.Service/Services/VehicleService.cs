using AutoMapper;
using Parking.Model.DTOs;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Interfaces.Services;
using Parking.Model.Models;

namespace Parking.Service.Services;

public class VehicleService : Service<Vehicle, VehicleDTO>, IVehicleService
{
    public VehicleService(IRepository<Vehicle> repository, IMapper mapper) : base(repository, mapper) { }
}
