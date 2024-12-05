namespace Identity.Application.DTOs.ApartmentService;
public class ApartmentDTO
{
    public string Id { get; set; } = null!;

    public float Area { get; set; }

    public string Description { get; set; } = null!;

    public int FloorNumber { get; set; }

    public int ApartmentNumber { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public ICollection<RelationshipDTO> Relationships { get; set; } = [];
}
