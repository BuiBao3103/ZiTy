
using Domain.Core.Models;

namespace Domain.Core.Repositories;

public interface IStatisticRepository
{
    public Task<int> GetTotalParticipantsSurveyAsync(int surveyId);
    public Task<List<QuestionStatistics>> GetAnswerStatisticsAsync(int surveyId);
}
