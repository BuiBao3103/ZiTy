using Survey.Application.DTOs.Questions;

namespace Survey.Application.DTOs.OtherAnswers;

public class OtherAnswerDTO
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? QuestionId { get; set; }

    public int? UserId { get; set; }

    public QuestionDTO? Question { get; set; }
}
