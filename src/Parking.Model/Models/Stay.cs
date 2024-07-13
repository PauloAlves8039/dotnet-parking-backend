using Parking.Model.Validation;

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

    public Stay() { }

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
        ValidateModel(customerVehicleId, licensePlate, entryDate, hourlyRate);

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

    private void ValidateModel(int? customerVehicleId, string licensePlate, DateTime? entryDate, decimal hourlyRate)
    {
        ModelExceptionValidation.When(customerVehicleId <= 0, "CustomerVehicleId must be greater than zero");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(licensePlate), "LicensePlate is required");
        ModelExceptionValidation.When(licensePlate.Length > 10, "LicensePlate cannot exceed 10 characters");
        ModelExceptionValidation.When(entryDate.HasValue && entryDate > DateTime.UtcNow, "EntryDate cannot be in the future");
        ModelExceptionValidation.When(hourlyRate <= 0, "HourlyRate must be greater than zero");
    }
}
