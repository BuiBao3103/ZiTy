using System.ComponentModel.DataAnnotations;
namespace Survey.Application.DTOs.Questions;

public class QuestionQueryDTO : BaseQueryDTO
{
    [RegularExpression(@"^(eq|neq|gt|gte|lt|lte|like|in):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? Id { get; set; }
    [RegularExpression(@"^(eq|like|in):[^\:]*$", ErrorMessage = "Invalid filter format")]
    public string? Content { get; set; }
    [RegularExpression(@"^(eq):[\w\s,]*$", ErrorMessage = "Invalid filter format")]
    public string? SurveyId { get; set; }
}
