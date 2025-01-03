using Application.DTOs.Users;

namespace Application.DTOs.Items;

public class ItemDTO
{
    public int Id { get; set; }

    public string? Image { get; set; }

    public string Description { get; set; } = null!;

    public bool? IsReceive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UserId { get; set; }

    public UserDTO? User { get; set; }
}

