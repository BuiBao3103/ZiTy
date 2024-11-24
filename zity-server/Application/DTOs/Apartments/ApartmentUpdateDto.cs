using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Apartments;

public class ApartmentUpdateDTO
{
    [Required]
    public float? Area { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public int? FloorNumber { get; set; }
    [Required]
    public int? ApartmentNumber { get; set; }
    [Required]
    public string Status { get; set; } = null!;
    [Required]
    public int? CurrentWaterNumber { get; set; }
}

