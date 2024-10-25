using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Answers;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class AnswerRepository(ApplicationDbContext dbContext) : IAnswerRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<Answer>> GetAllAsync(AnswerQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "Content", queryParam.Content },
                    { "QuestionId", queryParam.QuestionId?.ToString() }
                };
            var answersQuery = _dbContext.Answers
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await answersQuery;
        }

        public async Task<Answer?> GetByIdAsync(int id, string? includes)
        {
            var answersQuery = _dbContext.Answers.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await answersQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Answer> CreateAsync(Answer answer)
        {
            await _dbContext.Answers.AddAsync(answer);
            await _dbContext.SaveChangesAsync();
            return answer;
        }

        public async Task<Answer> UpdateAsync(Answer answer)
        {
            _dbContext.Answers.Update(answer);
            await _dbContext.SaveChangesAsync();
            return answer;
        }


        public async Task DeleteAsync(int id)
        {
            var answer = await _dbContext.Answers
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null) 
                ?? throw new EntityNotFoundException(nameof(Answer), id);
            answer.DeletedAt = DateTime.Now;
            _dbContext.Answers.Update(answer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
