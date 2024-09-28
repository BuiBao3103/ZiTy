using zity.DTOs.Users;

namespace zity.DTOs.Relationships
{
    public class RelationshipDTO
    {
        public int Id { get; set; }

        public string Role { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public string ApartmentId { get; set; } = null!;

        public UserDTO? User { get; set; } = null;

    }
}
