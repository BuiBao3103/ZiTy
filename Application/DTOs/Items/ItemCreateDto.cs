using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Items
{
    public class ItemCreateDTO
    {
        [Required]
        public string Image { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public bool IsReceive { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
