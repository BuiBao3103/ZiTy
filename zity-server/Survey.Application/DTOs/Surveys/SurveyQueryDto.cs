using System.ComponentModel.DataAnnotations;

namespace Survey.Application.DTOs.Surveys;

public class SurveyQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }
    [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? UserCreateId { get; set; }
    [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Title { get; set; }
}
