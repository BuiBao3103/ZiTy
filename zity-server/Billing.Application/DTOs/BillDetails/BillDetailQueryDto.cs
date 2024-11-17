using System.ComponentModel.DataAnnotations;

namespace Billing.Application.DTOs.BillDetails;

public class BillDetailQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }

    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? BillId { get; set; }

    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? ServiceId { get; set; }
}

