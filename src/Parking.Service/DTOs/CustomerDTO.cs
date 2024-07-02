using Parking.Model.Models;
using System.Text.Json.Serialization;

namespace Parking.Service.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }

    public string Name { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string Cpf { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int? AddressId { get; set; }

    [JsonIgnore]
    public virtual Address Address { get; set; }
}
