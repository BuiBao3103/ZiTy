using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.OtherAnswers;

public class OtherAnswerUpdateDTO
{
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    public int? QuestionId { get; set; }
    [Required]
    public int? UserId { get; set; }
}
