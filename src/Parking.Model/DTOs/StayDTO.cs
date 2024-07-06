using Parking.Model.Models;
using System.Text.Json.Serialization;

namespace Parking.Model.DTOs;

public class StayDTO
{
    public int Id { get; set; }

    public int? CustomerVehicleId { get; set; }

    public string LicensePlate { get; set; }

    public DateTime? EntryDate { get; set; }

    public DateTime? ExitDate { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string StayStatus { get; set; }

    [JsonIgnore]
    public virtual CustomerVehicle CustomerVehicle { get; set; }
}
