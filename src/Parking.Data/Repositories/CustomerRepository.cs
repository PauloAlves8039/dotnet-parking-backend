using Microsoft.Extensions.Logging;
using Parking.Data.Context;
using Parking.Model.Interfaces;
using Parking.Model.Models;

namespace Parking.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context, ILogger<Customer> logger) : base(context, logger) { }
    }
}
