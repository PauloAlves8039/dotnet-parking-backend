using Parking.Model.Models;
using System.Text.Json.Serialization;

namespace Parking.Model.DTOs;

public class CustomerVehicleDTO
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? VehicleId { get; set; }

    [JsonIgnore]
    public virtual Customer Customer { get; set; }

    [JsonIgnore]
    public virtual Vehicle Vehicle { get; set; }

    [JsonIgnore]
    public virtual ICollection<Stay> Stays { get; set; }
}