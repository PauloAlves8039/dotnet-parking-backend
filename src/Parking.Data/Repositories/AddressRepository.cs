using Microsoft.Extensions.Logging;
using Parking.Data.Context;
using Parking.Model.Interfaces;
using Parking.Model.Models;

namespace Parking.Data.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context, ILogger<Address> logger) : base(context, logger) { }
}
