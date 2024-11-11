using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Bills;

public class BillUpdateWaterReadingDto
{
    [Required]
    public List<WaterReadingDTO> WaterReadings { get; set; }
    public class WaterReadingDTO
    {
        [Required]
        public int? BillId { get; set; }
        [Required]
        public int? NewWaterIndex { get; set; }
        [Required]
        public DateTime? ReadingDate { get; set; }
    }
}
