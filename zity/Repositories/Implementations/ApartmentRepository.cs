using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Apartments;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class ApartmentRepository(ApplicationDbContext dbContext) : IApartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Apartment> CreateAsync(Apartment apartment)
        {

            await _dbContext.Apartments.AddAsync(apartment);
            await _dbContext.SaveChangesAsync();
            return apartment;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var apartment = await _dbContext.Apartments.
                FirstOrDefaultAsync(u => u.Id.Equals(id) && u.DeletedAt == null);
            if (apartment == null)
                return false;
            apartment.DeletedAt = DateTime.Now;
            _dbContext.Apartments.Update(apartment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedResult<Apartment>> GetAllAsync(ApartmentQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "FloorId", queryParam.FloorNumber },
                    { "ApartmentNumber", queryParam.ApartmentNumber }
                };
            var ApartmentsQuery = _dbContext.Apartments
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await ApartmentsQuery;
        }

        public async Task<Apartment?> GetByIdAsync(string id, string? includes)
        {
            var apartmentsQuery = _dbContext.Apartments.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await apartmentsQuery.FirstOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<Apartment> UpdateAsync(Apartment apartment)
        {
            _dbContext.Apartments.Update(apartment);
            await _dbContext.SaveChangesAsync();
            return apartment;
        }
    }
}
