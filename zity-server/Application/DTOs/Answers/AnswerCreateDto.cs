using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Answers;
public class AnswerCreateDTO
{
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    public int? QuestionId { get; set; }
}
