

using Application.DTOs.Apartments;
using Application.DTOs.Bills;
using Application.DTOs.Reports;
using Application.DTOs.Users;

namespace Application.DTOs.Relationships;

public class RelationshipDTO
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    public string ApartmentId { get; set; } = null!;

    public UserDTO? User { get; set; } = null;

    public ApartmentDTO? Apartment { get; set; } = null;

    public ICollection<BillDTO>? Bills { get; set; } = [];

    public ICollection<ReportDTO>? Reports { get; set; } = [];
}

