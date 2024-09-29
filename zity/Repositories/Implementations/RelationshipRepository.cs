using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Relationships;
using zity.ExceptionHandling;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace zity.Repositories.Implementations
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RelationshipRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<Relationship>> GetAllAsync(RelationshipQueryDTO query)
        {
            var filters = new Dictionary<string, string?>
                {
                    { "Id", query.Id },
                    { "UserId", query.UserId },
                    { "ApartmentId", query.ApartmentId }
                };
            var relationshipsQuery = _dbContext.Relationships.Where(u => u.DeletedAt == null)
                .ApplyIncludes(query.Includes)
                .ApplyFilters(filters)
                .ApplySorting(query.Sort)
                .ApplyPaginationAsync(query.Page, query.PageSize);

            return await relationshipsQuery;
        }

        public async Task<Relationship> GetByIdAsync(int id, string includes)
        {
            var relationshipsQuery = _dbContext.Relationships.Where(u => u.DeletedAt == null).ApplyIncludes(includes);
            var relationship = await relationshipsQuery.FirstOrDefaultAsync(u => u.Id == id);
            if (relationship == null)
            {
                throw new EntityNotFoundException($"Relationship with ID {id} not found.");
            }
            return relationship;
        }

        public async Task<Relationship> CreateAsync(Relationship relationship)
        {
            await _dbContext.Relationships.AddAsync(relationship);
            await _dbContext.SaveChangesAsync();
            return relationship;
        }

        public async Task DeleteAsync(int id)
        {
            var relationship = await _dbContext.Relationships.FirstOrDefaultAsync(u => u.Id == id);
            if (relationship == null)
            {
                throw new EntityNotFoundException($"Relationship with ID {id} not found.");
            }

            relationship.DeletedAt = DateTime.Now;
            _dbContext.Relationships.Update(relationship);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Relationship?> UpdateAsync(int id, Relationship relationship)
        {
            throw new NotImplementedException();
        }

        public Task<Relationship?> PatchAsync(int id, Relationship relationship)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRelationshipRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
