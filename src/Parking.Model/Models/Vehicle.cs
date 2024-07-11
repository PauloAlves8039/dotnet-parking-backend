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
        Id = id; ;
        VehicleType = vehicleType;
        Brand = brand;
        Model = model;
        Color = color;
        VehicleYear = vehicleYear;
        Notes = notes;
    }
}
