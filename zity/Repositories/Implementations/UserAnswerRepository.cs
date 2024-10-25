using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.UserAnswers;
using zity.ExceptionHandling;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class UserAnswerRepository(ApplicationDbContext dbContext) : IUserAnswerRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<UserAnswer>> GetAllAsync(UserAnswerQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "AnswerId", queryParam.AnswerId?.ToString() },
                    { "UserId", queryParam.UserId?.ToString() }
                };
            var userAnswersQuery = _dbContext.UserAnswers
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await userAnswersQuery;
        }

        public async Task<UserAnswer?> GetByIdAsync(int id, string? includes = null)
        {
            var userAnswersQuery = _dbContext.UserAnswers.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await userAnswersQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserAnswer> CreateAsync(UserAnswer userAnswer)
        {
            await _dbContext.UserAnswers.AddAsync(userAnswer);
            await _dbContext.SaveChangesAsync();
            return userAnswer;
        }

        public async Task<UserAnswer> UpdateAsync(UserAnswer userAnswer)
        {
            _dbContext.UserAnswers.Update(userAnswer);
            await _dbContext.SaveChangesAsync();
            return userAnswer;
        }


        public async Task DeleteAsync(int id)
        {
            var userAnswer = await _dbContext.UserAnswers
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null)
                ?? throw new EntityNotFoundException(nameof(UserAnswer), id);

            userAnswer.DeletedAt = DateTime.Now;
            _dbContext.UserAnswers.Update(userAnswer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
