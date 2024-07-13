using Parking.Model.Validation;

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
        ValidateModel(customerId, vehicleId);

        Id = id;
        CustomerId = customerId;
        VehicleId = vehicleId;
    }

    private void ValidateModel(int? customerId, int? vehicleId)
    {
        ModelExceptionValidation.When(!customerId.HasValue || customerId <= 0, "CustomerId is required and must be greater than zero");
        ModelExceptionValidation.When(!vehicleId.HasValue || vehicleId <= 0, "VehicleId is required and must be greater than zero");
    }
}
