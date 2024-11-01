using System.ComponentModel.DataAnnotations;
using zity.DTOs.BillDetails;

namespace zity.DTOs.Bills
{
    public class BillCreateDTO
    {
        [Required]
        public string Monthly { get; set; } = null!;

        [Required]
        public float TotalPrice { get; set; }

        public int? OldWater { get; set; }

        public int? NewWater { get; set; }

        public DateTime? WaterReadingDate { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public int RelationshipId { get; set; }

    }

}



