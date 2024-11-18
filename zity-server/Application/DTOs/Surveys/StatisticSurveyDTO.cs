

using Domain.Core.Models;

namespace Application.DTOs.Surveys
{
    public class StatisticSurveyDTO
    {
        public SurveyDTO Survey { get; set; } = null!;
        public int TotalParticipants { get; set; }
        public List<QuestionStatisticsDto> QuestionStatistics { get; set; } = null!;
    }
}
