namespace Application.DTOs.Services;

public class ServiceDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public float Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public object BillDetails { get; internal set; }

    // public ICollection<BillDetailDTO> BillDetails { get; set; } = [];
}
