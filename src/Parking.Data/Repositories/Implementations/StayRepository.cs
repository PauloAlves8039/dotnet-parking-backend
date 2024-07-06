using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Parking.Data.Context;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Models;

namespace Parking.Data.Repositories.Implementations;

public class StayRepository : Repository<Stay>, IStayRepository
{
    public StayRepository(ApplicationDbContext context, ILogger<Stay> logger) : base(context, logger) { }

    public override async Task<IEnumerable<Stay>> GetAllAsync()
    {
        try
        {
            var stays = await _context.Stays
                .Include(s => s.CustomerVehicle)
                    .ThenInclude(cv => cv.Customer)
                .Include(s => s.CustomerVehicle)
                    .ThenInclude(cv => cv.Vehicle)
                .Select(s => new Stay
                {
                    Id = s.Id,
                    CustomerVehicleId = s.CustomerVehicleId ?? 0,
                    LicensePlate = s.LicensePlate,
                    EntryDate = s.EntryDate,
                    ExitDate = s.ExitDate,
                    HourlyRate = s.HourlyRate,
                    TotalAmount = s.TotalAmount,
                    StayStatus = s.StayStatus,

                }).ToListAsync();


            return stays ?? new List<Stay>();
        }
        catch (Exception exception)
        {
            _errorMessage = $"Error when searching list of records: {exception.Message}";
            _logger.LogError(exception, _errorMessage);
            throw;
        }
    }

    public override async Task<Stay> GetByIdAsync(int id)
    {
        try
        {
            var stay = await _context.Stays
                .Include(s => s.CustomerVehicle)
                    .ThenInclude(cv => cv.Customer)
                .Include(s => s.CustomerVehicle)
                    .ThenInclude(cv => cv.Vehicle)
                .Select(s => new Stay
                {
                    Id = s.Id,
                    CustomerVehicleId = s.CustomerVehicleId ?? 0,
                    LicensePlate = s.LicensePlate,
                    EntryDate = s.EntryDate,
                    ExitDate = s.ExitDate,
                    HourlyRate = s.HourlyRate,
                    TotalAmount = s.TotalAmount,
                    StayStatus = s.StayStatus,

                }).FirstOrDefaultAsync();

            return stay;
        }
        catch (Exception exception)
        {
            _errorMessage = $"Error getting record with ID: {exception.Message}";
            _logger.LogError(exception, _errorMessage);
            throw;
        }
    }

    public override async Task<Stay> UpdateAsync(Stay stay)
    {
        try
        {
            var existingStay = await _context.Stays.FirstOrDefaultAsync(s => s.Id == stay.Id);

            if (existingStay != null)
            {
                existingStay.CustomerVehicle = stay.CustomerVehicle;
                existingStay.LicensePlate = stay.LicensePlate;
                existingStay.EntryDate = stay.EntryDate;
                existingStay.ExitDate = stay.ExitDate;
                existingStay.HourlyRate = stay.HourlyRate;
                existingStay.TotalAmount = stay.TotalAmount;
                existingStay.StayStatus = stay.StayStatus;

                await _context.SaveChangesAsync();
            }

            return existingStay;
        }
        catch (Exception exception)
        {
            _errorMessage = $"Error when updating the record: {exception.Message}";
            _logger.LogError(exception, _errorMessage);
            throw;
        }
    }
}
