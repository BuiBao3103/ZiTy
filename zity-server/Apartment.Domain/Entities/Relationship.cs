using Apartment.Domain.Core.Models;

namespace Apartment.Domain.Entities;
public partial class Relationship : BaseEntity
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int UserId { get; set; }

    public string ApartmentId { get; set; } = null!;

    public virtual Apartment Apartment { get; set; } = null!;

}
