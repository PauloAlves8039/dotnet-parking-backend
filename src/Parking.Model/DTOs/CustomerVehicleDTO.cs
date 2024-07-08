using Parking.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Parking.Model.DTOs;

public class CustomerVehicleDTO
{
    [Range(0, int.MaxValue, ErrorMessage = "Invalid Id value")]
    public int Id { get; set; }

    [DisplayName("Customer Id")]
    public int? CustomerId { get; set; }

    [DisplayName("Vehicle Id")]
    public int? VehicleId { get; set; }

    [JsonIgnore]
    public virtual Customer Customer { get; set; }

    [JsonIgnore]
    public virtual Vehicle Vehicle { get; set; }

    [JsonIgnore]
    public virtual ICollection<Stay> Stays { get; set; }
}