using Parking.Model.DTOs;
using Parking.Model.Interfaces.Repositories;
using Parking.Model.Models;

namespace Parking.Service.Helpers.Interfaces
{
    public interface IUtilities
    {
        Task UpdateStayStatus(int id, string newStatus, IRepository<Stay> repository);
        double CalculateStayHours(StayDTO stayDTO);
        decimal CalculateTotalAmount(double hours, decimal hourlyRate);
    }
}
