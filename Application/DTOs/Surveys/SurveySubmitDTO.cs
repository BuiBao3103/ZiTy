using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Surveys
{
    public class SurveySubmitDTO
    {
        [Required]
        public int? UserId { get; set; }
        [Required]
        public List<int> AnswerIds { get; set; }
        [Required]
        public List<string> OtherAnswers { get; set; }
    }
}
