namespace Parking.Model.Models;

public partial class Address : Entity
{
    public string Street { get; set; }

    public string Number { get; set; }

    public string? Complement { get; set; }

    public string Neighborhood { get; set; }

    public string FederativeUnit { get; set; }

    public string City { get; set; }

    public string ZipCode { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public Address(int id, 
                   string street, 
                   string number, 
                   string complement, 
                   string neighborhood, 
                   string federativeUnit, 
                   string city, 
                   string zipCode)
    {
        Id = id;
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        FederativeUnit = federativeUnit;
        City = city;
        ZipCode = zipCode;
    }
}
