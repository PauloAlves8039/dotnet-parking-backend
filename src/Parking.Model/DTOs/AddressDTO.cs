using System.ComponentModel.DataAnnotations;

namespace Parking.Model.DTOs;

public class AddressDTO
{
    [Range(0, int.MaxValue, ErrorMessage = "Invalid Id value")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [MaxLength(100)]
    [MinLength(3, ErrorMessage = "Street must be at least 3 characters long")]
    [Display(Name = "Street")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Number is required")]
    [MaxLength(10)]
    [MinLength(1, ErrorMessage = "Number must be at least 1 character long")]
    [Display(Name = "Number")]
    public string Number { get; set; }

    [MaxLength(150)]
    [Display(Name = "Complement")]
    public string? Complement { get; set; }

    [Required(ErrorMessage = "Neighborhood is required")]
    [MaxLength(100)]
    [MinLength(3, ErrorMessage = "Neighborhood must be at least 3 characters long")]
    [Display(Name = "Neighborhood")]
    public string Neighborhood { get; set; }

    [Required(ErrorMessage = "FederativeUnit is required")]
    [MaxLength(2)]
    [Display(Name = "Federative Unit")]
    public string FederativeUnit { get; set; }

    [Required(ErrorMessage = "City is required")]
    [MaxLength(100)]
    [MinLength(3, ErrorMessage = "City must be at least 3 characters long")]
    [Display(Name = "City")]
    public string City { get; set; }

    [Required(ErrorMessage = "ZipCode is required")]
    [MaxLength(9)]
    [MinLength(8, ErrorMessage = "ZipCode must be at least 8 characters long")]
    [Display(Name = "Zip Code")]
    public string ZipCode { get; set; }
}