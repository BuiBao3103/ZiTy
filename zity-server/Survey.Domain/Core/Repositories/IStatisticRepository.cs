using Survey.Domain.Core.Models;

namespace Survey.Domain.Core.Repositories;

public interface IStatisticRepository
{
    public Task<int> GetTotalParticipantsSurveyAsync(int surveyId);
    public Task<List<QuestionStatistics>> GetAnswerStatisticsAsync(int surveyId);
}
