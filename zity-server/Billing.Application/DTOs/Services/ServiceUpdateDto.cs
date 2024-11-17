using System.ComponentModel.DataAnnotations;

namespace Billing.Application.DTOs.Services;

public class ServiceUpdateDTO
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public float? Price { get; set; }
}

