using Microsoft.Extensions.Logging;
using Parking.Data.Context;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Models;

namespace Parking.Data.Repositories.Implementations;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context, ILogger<Address> logger) : base(context, logger) { }
}
