using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserAnswers;

public class UserAnswerQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }
    [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public int? AnswerId { get; set; }
    [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public int? UserId { get; set; }
}
