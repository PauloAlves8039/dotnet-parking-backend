namespace Parking.Model.Models;

public partial class Stay : Entity
{
    public int? CustomerVehicleId { get; set; }

    public string LicensePlate { get; set; }

    public DateTime? EntryDate { get; set; }

    public DateTime? ExitDate { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string StayStatus { get; set; }

    public virtual CustomerVehicle CustomerVehicle { get; set; }
}
