using Parking.Model.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Parking.Model.DTOs;

public class StayDTO
{
    [Range(0, int.MaxValue, ErrorMessage = "Invalid Id value")]
    public int Id { get; set; }

    [Display(Name = "Customer Vehicle Id")]
    public int? CustomerVehicleId { get; set; }

    [Required(ErrorMessage = "License Plate is required")]
    [MaxLength(10)]
    [MinLength(3, ErrorMessage = "License Plate must be at least 3 character long")]
    [Display(Name = "License Plate")]
    public string LicensePlate { get; set; }

    [Required(ErrorMessage = "Entry Date is required")]
    [Display(Name = "Entry Date")]
    public DateTime? EntryDate { get; set; }

    [Display(Name = "Exit Date")]
    public DateTime? ExitDate { get; set; }

    [Required(ErrorMessage = "Hourly Rate is required")]
    [Display(Name = "Hourly Rate")]
    public decimal HourlyRate { get; set; }

    [Display(Name = "Total Amount")]
    public decimal? TotalAmount { get; set; }

    [Display(Name = "Stay Status")]
    public string StayStatus { get; set; }

    [JsonIgnore]
    public virtual CustomerVehicle CustomerVehicle { get; set; }
}
