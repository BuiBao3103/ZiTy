using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Relationships;
using zity.ExceptionHandling;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class RelationshipRepository(ApplicationDbContext dbContext) : IRelationshipRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<Relationship>> GetAllAsync(RelationshipQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "UserId", queryParam.UserId },
                    { "ApartmentId", queryParam.ApartmentId }
                };
            var relationshipsQuery = _dbContext.Relationships
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await relationshipsQuery;
        }

        public async Task<Relationship?> GetByIdAsync(int id, string? includes)
        {
            var relationshipsQuery = _dbContext.Relationships.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await relationshipsQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Relationship> CreateAsync(Relationship relationship)
        {
            await _dbContext.Relationships.AddAsync(relationship);
            await _dbContext.SaveChangesAsync();
            return relationship;
        }

        public async Task<Relationship> UpdateAsync(Relationship relationship)
        {
            _dbContext.Relationships.Update(relationship);
            await _dbContext.SaveChangesAsync();
            return relationship;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var relationship = await _dbContext.Relationships
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
            if (relationship == null)
            {
                return false;
            }
            relationship.DeletedAt = DateTime.Now;
            _dbContext.Relationships.Update(relationship);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        
    }
}
