using Parking.Model.Validation;

namespace Parking.Model.Models;

public partial class Vehicle : Entity
{
    public string VehicleType { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public string Color { get; set; }

    public int? VehicleYear { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<CustomerVehicle> CustomerVehicles { get; set; } = new List<CustomerVehicle>();

    public Vehicle(int id, 
                   string vehicleType, 
                   string brand, 
                   string model, 
                   string color, 
                   int? vehicleYear, 
                   string notes)
    {
        ValidateModel(vehicleType, brand, model, color, vehicleYear, notes);

        Id = id;
        VehicleType = vehicleType;
        Brand = brand;
        Model = model;
        Color = color;
        VehicleYear = vehicleYear;
        Notes = notes;
    }

    private void ValidateModel(string vehicleType,
                               string brand,
                               string model,
                               string color,
                               int? vehicleYear,
                               string? notes)
    {
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(vehicleType), "VehicleType is required");
        ModelExceptionValidation.When(vehicleType.Length > 10, "VehicleType cannot exceed 10 characters");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(brand), "Brand is required");
        ModelExceptionValidation.When(brand.Length > 50, "Brand cannot exceed 50 characters");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(model), "Model is required");
        ModelExceptionValidation.When(model.Length > 50, "Model cannot exceed 50 characters");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(color), "Color is required");
        ModelExceptionValidation.When(color.Length > 50, "Color cannot exceed 50 characters");

        if (vehicleYear.HasValue)
        {
            ModelExceptionValidation.When(vehicleYear < 1900 || vehicleYear > 2100, "VehicleYear must be between 1900 and 2100");
        }

        if (!string.IsNullOrEmpty(notes))
        {
            ModelExceptionValidation.When(notes.Length > 200, "Notes cannot exceed 200 characters");
        }
    }

}
