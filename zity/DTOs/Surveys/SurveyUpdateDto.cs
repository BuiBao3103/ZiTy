using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Surveys
{
    public class SurveyUpdateDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int TotalQuestions { get; set; }
        [Required]
        public int? UserCreateId { get; set; }
    }
}
