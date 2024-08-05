using Parking.Model.DTOs;

namespace Parking.Service.Helpers.Interfaces;

public interface IPdfService
{
    byte[] GenerateStayPdf(StayDTO stayDto);
}
