using Parking.Model.Validation;

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

    public Customer(int id, string name, DateOnly? birthDate, string cpf, string phone, string email, int? addressId)
    {
        ValidateModel(name, birthDate, cpf, phone, email, addressId);

        Id = id;
        Name = name;
        BirthDate = birthDate;
        Cpf = cpf;
        Phone = phone;
        Email = email;
        AddressId = addressId;
    }

    private void ValidateModel(string name, DateOnly? birthDate, string cpf, string phone, string email, int? addressId)
    {
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(name), "Name is required");
        ModelExceptionValidation.When(name.Length > 100, "Name cannot exceed 100 characters");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(cpf), "CPF is required");
        ModelExceptionValidation.When(cpf.Length != 11, "CPF must be 11 characters long");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(phone), "Phone is required");
        ModelExceptionValidation.When(phone.Length > 15, "Phone cannot exceed 15 characters");
        ModelExceptionValidation.When(string.IsNullOrWhiteSpace(email), "Email is required");
        ModelExceptionValidation.When(email.Length > 100, "Email cannot exceed 100 characters");

        if (birthDate.HasValue)
        {
            string birthDateString = birthDate.Value.ToString("dd/MM/yyyy");
            ModelExceptionValidation.When(!DateTime.TryParseExact(birthDateString, "dd/MM/yyyy", null, 
                System.Globalization.DateTimeStyles.None, out _), "BirthDate must be in the format DD/MM/YYYY");
        }

        ModelExceptionValidation.When(addressId <= 0 && addressId.HasValue, "AddressId must be greater than zero when provided");
    }
}
