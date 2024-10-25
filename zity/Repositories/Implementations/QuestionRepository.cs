using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Questions;
using zity.ExceptionHandling;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class QuestionRepository(ApplicationDbContext dbContext) : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<Question>> GetAllAsync(QuestionQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "Content", queryParam.Content },
                    { "SurveyId", queryParam.SurveyId?.ToString() }
                };
            var questionsQuery = _dbContext.Questions
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await questionsQuery;
        }

        public async Task<Question?> GetByIdAsync(int id, string? includes = null)
        {
            var questionsQuery = _dbContext.Questions.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await questionsQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Question> CreateAsync(Question question)
        {
            await _dbContext.Questions.AddAsync(question);
            await _dbContext.SaveChangesAsync();
            return question;
        }

        public async Task<Question> UpdateAsync(Question question)
        {
            _dbContext.Questions.Update(question);
            await _dbContext.SaveChangesAsync();
            return question;
        }


        public async Task DeleteAsync(int id)
        {
            var question = await _dbContext.Questions
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null)
                                ?? throw new EntityNotFoundException(nameof(Question), id);

            question.DeletedAt = DateTime.Now;
            _dbContext.Questions.Update(question);
            await _dbContext.SaveChangesAsync();
        }
    }
}
