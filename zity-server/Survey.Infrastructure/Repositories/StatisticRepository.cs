using Survey.Application.DTOs.Surveys;
using Survey.Domain.Core.Models;
using Survey.Domain.Core.Repositories;
using Survey.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Survey.Infrastructure.Repositories
{
    public class StatisticRepository(SurveyDbContext dbContext) : IStatisticRepository
    {
        private readonly SurveyDbContext _dbContext = dbContext;
        public async Task<int> GetTotalParticipantsSurveyAsync(int surveyId)
        {
            // Get users who selected predefined answers
            var usersFromAnswers = _dbContext.UserAnswers
                .Where(ua => ua.Answer.Question.SurveyId == surveyId)
                .Select(ua => ua.UserId);

            // Get users who created other answers
            var usersFromOtherAnswers = _dbContext.OtherAnswers
                .Where(oa => oa.Question.SurveyId == surveyId)
                .Select(oa => oa.UserId);

            // Combine both sets of users and count distinct
            return await usersFromAnswers
                .Union(usersFromOtherAnswers)
                .Distinct()
                .CountAsync();
        }

        public async Task<List<QuestionStatistics>> GetAnswerStatisticsAsync(int surveyId)
        {
            return await _dbContext.Questions
                .Where(q => q.SurveyId == surveyId)
                .Select(q => new QuestionStatistics
                {
                    QuestionId = q.Id,
                    QuestionContent = q.Content,
                    Answers = q.Answers.Select(a => new AnswerStatistics
                    {
                        AnswerId = a.Id,
                        Content = a.Content,
                        Count = a.UserAnswers.Count
                    }).ToList(),
                    OtherAnswers = q.OtherAnswers.Select(oa => new OtherAnswerStatistics
                    {
                        Id = oa.Id,
                        Content = oa.Content
                    }).ToList()
                })
                .ToListAsync();
        } 

      

        //public async Task<List<MonthlyRevenueStatistics>> GetStatisticsRevenue(string startMonth, string endMonth)
        //{
        //    return await _dbContext.Bills
        //      .Where(b => b.Monthly.CompareTo(startMonth) >= 0 && b.Monthly.CompareTo(endMonth) <= 0 && b.Status == "PAID")
        //      .GroupBy(b => b.Monthly)
        //      .Select(g => new MonthlyRevenueStatistics
        //      {
        //          Month = g.Key,
        //          TotalRevenue = g.Sum(b => (decimal)b.TotalPrice)
        //      })
        //      .OrderBy(mr => mr.Month)
        //      .ToListAsync();
        //}
    }
}
