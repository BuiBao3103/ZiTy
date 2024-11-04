namespace Application.DTOs.Surveys;

public class SurveyPatchDTO
{
    public string? Title { get; set; }
    public DateTime? EndDate { get; set; }
    public int? TotalQuestions { get; set; }
    public int? UserCreateId { get; set; }
}
