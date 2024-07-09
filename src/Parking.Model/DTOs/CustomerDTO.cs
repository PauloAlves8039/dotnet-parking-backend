using Parking.Model.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Parking.Model.DTOs;

public class CustomerDTO
{
    [Range(0, int.MaxValue, ErrorMessage = "Invalid Id value")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100)]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
    [DisplayName("Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Birth Date is required")]
    [DisplayName("Birth Date")]
    [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
    public DateOnly? BirthDate { get; set; }

    [Required(ErrorMessage = "CPF is required")]
    [MaxLength(11)]
    [MinLength(11, ErrorMessage = "CPF must be 11 characters long")]
    [DisplayName("CPF")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [MaxLength(15)]
    [MinLength(9, ErrorMessage = "Phone must be between 9 and 15 characters long")]
    [DisplayName("Phone")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100)]
    [MinLength(10, ErrorMessage = "Email must be between 10 and 100 characters long")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [DisplayName("Email")]
    public string Email { get; set; }

    [DisplayName("Address Id")]
    public int? AddressId { get; set; }

    [JsonIgnore]
    public virtual Address Address { get; set; }
}
