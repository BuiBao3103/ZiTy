using zity.Models;

namespace zity.DTOs.Services
{
    public class ServiceDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public float Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<BillDetail> BillDetails { get; set; } = [];
    }
}
