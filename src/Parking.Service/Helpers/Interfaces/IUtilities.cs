using Parking.Model.Interfaces;
using Parking.Model.Models;
using Parking.Service.DTOs;

namespace Parking.Service.Helpers.Interfaces
{
    public interface IUtilities
    {
        Task UpdateStayStatus(int id, string newStatus, IRepository<Stay> repository);
        double CalculateStayHours(StayDTO stayDTO);
        decimal CalculateTotalAmount(double hours, decimal hourlyRate);
    }
}
