namespace Parking.Model.Models;

public partial class Customer : Entity
{
    public string Name { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string Cpf { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int? AddressId { get; set; }

    public virtual Address Address { get; set; }

    public virtual ICollection<CustomerVehicle> CustomerVehicles { get; set; } = new List<CustomerVehicle>();

    public Customer(int id, 
                    string name, 
                    DateOnly? birthDate, 
                    string cpf, 
                    string phone, 
                    string email, 
                    int? addressId)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
        Cpf = cpf;
        Phone = phone;
        Email = email;
        AddressId = addressId;
    }
}
