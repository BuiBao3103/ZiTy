using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Apartments
{
    public class ApartmentCreateDTO
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public float Area { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int FloorNumber { get; set; }
        [Required]
        public int ApartmentNumber { get; set; }
        [Required]
        public string Status { get; set; } = null!;
    }
}
