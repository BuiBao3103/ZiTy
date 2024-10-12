using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Services
{
    public class ServiceCreateDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public float Price { get; set; }

    }
}
