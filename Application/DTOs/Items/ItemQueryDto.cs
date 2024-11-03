using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Items;

public class ItemQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }
    [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? UserId { get; set; }
}
