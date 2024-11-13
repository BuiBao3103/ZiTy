using Survey.Application.DTOs.OtherAnswers;
using Survey.Application.DTOs.UserAnswers;
using System.ComponentModel.DataAnnotations;

namespace Survey.Application.DTOs.Surveys;

public class SurveySubmitDTO
{
    [Required]
    public List<UserAnswerCreateDTO> UserAnswers { get; set; }
    [Required]
    public List<OtherAnswerCreateDTO> OtherAnswers { get; set; }
}


