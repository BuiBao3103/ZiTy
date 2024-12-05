using Apartment.Application.DTOs.Relationships;

namespace Apartment.Application.DTOs.Apartments;
public class ApartmentDTO
{
    public string Id { get; set; } = null!;

    public float Area { get; set; }

    public string Description { get; set; } = null!;

    public int FloorNumber { get; set; }

    public int ApartmentNumber { get; set; }

    public string Status { get; set; } = null!;
    public int CurrentWaterNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public ICollection<RelationshipDTO> Relationships { get; set; } = [];
}
