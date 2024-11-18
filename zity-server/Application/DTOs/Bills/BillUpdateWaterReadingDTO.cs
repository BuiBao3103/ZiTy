using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Bills;

public class BillUpdateWaterReadingDto
{
    [Required]
    public List<WaterReadingDTO> WaterReadings { get; set; } = null!;
    public class WaterReadingDTO
    {
        [Required]
        public int? BillId { get; set; } = null!;
        [Required]
        public int? NewWaterIndex { get; set; } = null!;
        [Required]
        public DateTime? ReadingDate { get; set; } = null!;
    }
}
