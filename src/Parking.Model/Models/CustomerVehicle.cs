namespace Parking.Model.Models;

public partial class CustomerVehicle : Entity
{
    public int? CustomerId { get; set; }

    public int? VehicleId { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<Stay> Stays { get; set; } = new List<Stay>();

    public virtual Vehicle Vehicle { get; set; }

    public CustomerVehicle(int id, int? customerId, int? vehicleId)
    {
        Id = id;
        CustomerId = customerId;
        VehicleId = vehicleId;
    }
}
