using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Surveys;

public class SurveySubmitDTO
{
    [Required]
    public int? UserId { get; set; }
    [Required]
    public List<int> AnswerIds { get; set; }
    [Required]
    public List<OtherAnswer> OtherAnswers { get; set; }
    public class OtherAnswer
    {
        public string Content { get; set; }
        public int QuestionId { get; set; }
    }
}


