using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parking.Data.Context;
using Parking.Model.Interfaces;
using Parking.Model.Models;

namespace Parking.Data.Repositories
{
    public class CustomerVehicleRepository : Repository<CustomerVehicle>, ICustomerVehicleRepository
    {
        public CustomerVehicleRepository(ApplicationDbContext context, ILogger<CustomerVehicle> logger) : base(context, logger) { }

        public override async Task<IEnumerable<CustomerVehicle>> GetAllAsync()
        {
            try
            {
                var details = await _context.CustomerVehicles
                    .Include(cv => cv.Customer)
                    .Include(cv => cv.Vehicle)
                    .ToListAsync();

                foreach (var detail in details)
                {
                    var customerName = detail.Customer.Name;
                    var vehicleName = detail.Vehicle.Model;
                }

                return details;
            }
            catch (Exception exception)
            {
                _errorMessage = $"Error when searching list of records: {exception.Message}";
                _logger.LogError(exception, _errorMessage);
                throw;
            }
        }
    }
}
