using Parking.Model.Validation;

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
        ValidateModel(street, number, complement, neighborhood, federativeUnit, city, zipCode);

        Id = id;
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        FederativeUnit = federativeUnit;
        City = city;
        ZipCode = zipCode;
    }

    private void ValidateModel(string street, 
                               string number, 
                               string complement, 
                               string neighborhood, 
                               string federativeUnit, 
                               string city, 
                               string zipCode) 
    {
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(street), "Street is required");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(number), "Number is required");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(neighborhood), "Neighborhood is required");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(federativeUnit), "FederativeUnit is required");
        ModelExceptionValidation.When(federativeUnit.Length != 2, "FederativeUnit must be 2 characters long");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(city), "City is required");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(zipCode), "ZipCode is required");
        ModelExceptionValidation.When(zipCode.Length != 9, "ZipCode must be 9 characters long");
        
        if (complement != null)
        {
            ModelExceptionValidation.When(complement.Length > 150, "Complement cannot exceed 150 characters");
        }
    }
}
