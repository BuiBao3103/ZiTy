namespace Billing.Application.DTOs.ApartmentService;

public class RelationshipDTO
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    public string ApartmentId { get; set; } = null!;

    public ApartmentDTO? Apartment { get; set; } = null;
}


public class RelationshipResponse
{
    public List<RelationshipDTO> Contents { get; set; }
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
