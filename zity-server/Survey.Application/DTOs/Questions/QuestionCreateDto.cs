using System.ComponentModel.DataAnnotations;
namespace Survey.Application.DTOs.Questions;

public class QuestionCreateDTO
{
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    public int? SurveyId { get; set; }
}
