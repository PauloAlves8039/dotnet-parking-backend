namespace Parking.Model.DTOs;

public class VehicleDTO
{
    public int Id { get; set; }

    public string VehicleType { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public string Color { get; set; }

    public int? VehicleYear { get; set; }

    public string? Notes { get; set; }
}
