using Survey.Application.DTOs.Questions;

namespace Survey.Application.DTOs.Surveys;

public class SurveyDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int TotalQuestions { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UserCreateId { get; set; }

    public ICollection<QuestionDTO> Questions { get; set; } = [];
}
