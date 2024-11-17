namespace Apartment.Application.DTOs.Relationships;

public class RelationshipPatchDTO
{
    public string? Role { get; set; }
    public int? UserId { get; set; }
    public string? ApartmentId { get; set; }
}

