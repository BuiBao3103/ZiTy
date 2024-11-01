using System.ComponentModel.DataAnnotations;
namespace zity.DTOs.Questions
{
    public class QuestionUpdateDTO
    {
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public int SurveyId { get; set; }
    }
}
