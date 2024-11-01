using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.BillDetails
{
    public class BillDetailUpdateDTO
    {
        [Required]
        public float Price { get; set; }

        [Required]
        public int BillId { get; set; }

        [Required]
        public int ServiceId { get; set; }
    }
}
