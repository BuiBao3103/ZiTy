using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Items;

public class ItemCreateDTO
{
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public int? UserId { get; set; }
}

