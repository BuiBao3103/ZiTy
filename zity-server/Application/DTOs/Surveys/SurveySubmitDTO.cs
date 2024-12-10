using Application.DTOs.OtherAnswers;
using Application.DTOs.UserAnswers;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Surveys;

public class SurveySubmitDTO
{
    [Required]
    public List<UserAnswerCreateDTO> UserAnswers { get; set; }
    [Required]
    public List<OtherAnswerCreateDTO> OtherAnswers { get; set; }
}


