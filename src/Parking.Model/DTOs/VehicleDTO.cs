using System.ComponentModel.DataAnnotations;

namespace Parking.Model.DTOs;

public class VehicleDTO
{
    [Range(0, int.MaxValue, ErrorMessage = "Invalid Id value")]
    public int Id { get; set; }

    [MaxLength(10)]
    [MinLength(1, ErrorMessage = "Vehicle Type must be at least 1 character long")]
    [Display(Name = "Vehicle Type")]
    public string VehicleType { get; set; }

    [Required(ErrorMessage = "Brand is required")]
    [MaxLength(50)]
    [MinLength(1, ErrorMessage = "Brand must be at least 1 character long")]
    [Display(Name = "Brand")]
    public string Brand { get; set; }

    [Required(ErrorMessage = "Model is required")]
    [MaxLength(50)]
    [MinLength(1, ErrorMessage = "Model must be at least 1 character long")]
    [Display(Name = "Model")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Color is required")]
    [MaxLength(50)]
    [MinLength(1, ErrorMessage = "Color must be at least 1 character long")]
    [Display(Name = "Color")]
    public string Color { get; set; }

    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
    [Display(Name = "Vehicle Year")]
    public int? VehicleYear { get; set; }

    [MaxLength(200)]
    [MinLength(1, ErrorMessage = "Notes must be at least 1 character long")]
    [Display(Name = "Notes")]
    public string? Notes { get; set; }
}
