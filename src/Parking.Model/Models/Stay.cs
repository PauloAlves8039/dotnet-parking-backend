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

    public Stay(int id,
                int? customerVehicleId, 
                string licensePlate, 
                DateTime? entryDate, 
                DateTime? exitDate, 
                decimal hourlyRate, 
                decimal? totalAmount, 
                string stayStatus, 
                CustomerVehicle customerVehicle)
    {
        Id = id;
        CustomerVehicleId = customerVehicleId;
        LicensePlate = licensePlate;
        EntryDate = entryDate;
        ExitDate = exitDate;
        HourlyRate = hourlyRate;
        TotalAmount = totalAmount;
        StayStatus = stayStatus;
        CustomerVehicle = customerVehicle;
    }
}
