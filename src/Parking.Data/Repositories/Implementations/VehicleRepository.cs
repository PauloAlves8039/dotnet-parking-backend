using Microsoft.Extensions.Logging;
using Parking.Data.Context;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Models;

namespace Parking.Data.Repositories.Implementations;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext context, ILogger<Vehicle> logger) : base(context, logger) { }
}
