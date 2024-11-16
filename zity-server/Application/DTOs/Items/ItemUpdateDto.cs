using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Items;

public class ItemUpdateDTO
{
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public bool IsReceive { get; set; }
    [Required]
    public int? UserId { get; set; }
}

