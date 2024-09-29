using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Relationships;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IncludeHandler<Relationship> _includeHandler = new();
        private readonly FilterHandler<Relationship> _filterHandler = new();
        private readonly SortHandler<Relationship> _sortHandler = new();
        private readonly PaginationHandler<Relationship> _paginationHandler = new();

        public RelationshipRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<Relationship>> GetAllAsync(RelationshipQueryDTO query)
        {
            var relationshipsQuery = _dbContext.Relationships.Where(u => u.DeletedAt == null);
            relationshipsQuery = _includeHandler.ApplyIncludes(relationshipsQuery, query.Includes);
            var filters = new Dictionary<string, string>
                {
                    { "Id", query.Id },
                    { "UserId", query.UserId },
                    { "ApartmentId", query.ApartmentId }
                };

            relationshipsQuery = _filterHandler.ApplyFilters(relationshipsQuery, filters);
            relationshipsQuery = _sortHandler.ApplySorting(relationshipsQuery, query.Sort);

            return await _paginationHandler.ApplyPaginationAsync(relationshipsQuery, query.Page, query.PageSize);
        }

        public async Task<Relationship> GetByIdAsync(int id, string includes)
        {
            var relationshipsQuery = _dbContext.Relationships.Where(u => u.DeletedAt == null);
            relationshipsQuery = _includeHandler.ApplyIncludes(relationshipsQuery, includes);
            return await relationshipsQuery.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
