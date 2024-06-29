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
}
