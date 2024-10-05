using zity.Models;

namespace zity.DTOs.Surveys
{
    public class SurveyPatchDTO
    {
        public string? Title { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TotalQuestions { get; set; }
        public User? UserCreate { get; set; }
    }
}
