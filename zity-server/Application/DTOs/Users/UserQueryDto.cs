using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users;
public class UserQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string Id { get; set; } = "";

    [RegularExpression(@"^(eq|neq|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string Username { get; set; } = "";
}
