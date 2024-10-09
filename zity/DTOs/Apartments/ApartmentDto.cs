using zity.DTOs.Relationships;
<<<<<<< HEAD
=======
using zity.Models;
>>>>>>> c1ccd1ae38cb1f0fa7de38e4e8c95d43d00ce767

namespace zity.DTOs.Apartments
{
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

<<<<<<< HEAD
        public DateTime? DeletedAt { get; set; }

        public ICollection<RelationshipDTO>? Relationships { get; set; } = [];
=======
        public ICollection<RelationshipDTO> Relationships { get; set; } = [];

>>>>>>> c1ccd1ae38cb1f0fa7de38e4e8c95d43d00ce767
    }
}
