namespace Survey.Application.DTOs.OtherAnswers;

public class OtherAnswerPatchDTO
{
    public string? Content { get; set; }
    public int? QuestionId { get; set; }
    public int? UserId { get; set; }
}
