using Parking.Model.DTOs;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Models;
using Parking.Service.Helpers.Interfaces;

namespace Parking.Service.Helpers;

public class Utilities : IUtilities
{
    public async Task UpdateStayStatus(int id, string newStatus, IRepository<Stay> repository)
    {
        var existingStay = await repository.GetByIdAsync(id);

        if (existingStay != null)
        {
            existingStay.StayStatus = newStatus;
            await repository.UpdateAsync(existingStay);
        }
    }


    public double CalculateStayHours(StayDTO stayDTO)
    {
        if (stayDTO.EntryDate == null || stayDTO.ExitDate == null)
        {
            return 0;
        }

        TimeSpan hoursDifference = stayDTO.ExitDate.Value - stayDTO.EntryDate.Value;
        return hoursDifference.TotalHours;
    }

    public decimal CalculateTotalAmount(double hours, decimal hourlyRate)
    {
        decimal totalAmount = (decimal)hours * hourlyRate;
        return totalAmount;
    }
}
