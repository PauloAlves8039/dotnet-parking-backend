namespace Parking.Model.DTOs;

public class AddressDTO
{
    public int Id { get; set; }

    public string Street { get; set; }

    public string Number { get; set; }

    public string? Complement { get; set; }

    public string Neighborhood { get; set; }

    public string FederativeUnit { get; set; }

    public string City { get; set; }

    public string ZipCode { get; set; }
}