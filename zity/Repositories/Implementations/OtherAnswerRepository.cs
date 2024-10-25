using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.OtherAnswers;
using zity.ExceptionHandling;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class OtherAnswerRepository(ApplicationDbContext dbContext) : IOtherAnswerRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<OtherAnswer>> GetAllAsync(OtherAnswerQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "Content", queryParam.Content },
                    { "QuestionId", queryParam.QuestionId?.ToString() },
                    { "UserId", queryParam.UserId?.ToString() }
                };
            var otherAnswersQuery = _dbContext.OtherAnswers
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await otherAnswersQuery;
        }

        public async Task<OtherAnswer?> GetByIdAsync(int id, string? includes = null)
        {
            var otherAnswersQuery = _dbContext.OtherAnswers.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await otherAnswersQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<OtherAnswer> CreateAsync(OtherAnswer otherAnswer)
        {
            await _dbContext.OtherAnswers.AddAsync(otherAnswer);
            await _dbContext.SaveChangesAsync();
            return otherAnswer;
        }

        public async Task<OtherAnswer> UpdateAsync(OtherAnswer otherAnswer)
        {
            _dbContext.OtherAnswers.Update(otherAnswer);
            await _dbContext.SaveChangesAsync();
            return otherAnswer;
        }


        public async Task DeleteAsync(int id)
        {
            var otherAnswer = await _dbContext.OtherAnswers
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null)
                                ?? throw new EntityNotFoundException(nameof(OtherAnswer), id);

            otherAnswer.DeletedAt = DateTime.Now;
            _dbContext.OtherAnswers.Update(otherAnswer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
