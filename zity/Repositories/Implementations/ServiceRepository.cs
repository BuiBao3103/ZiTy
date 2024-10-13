using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Services;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;

namespace zity.Repositories.Implementations
{
    public class ServiceRepository(ApplicationDbContext context) : IServiceRepository
    {
        private readonly ApplicationDbContext _dbContext = context;

        public async Task<PaginatedResult<Service>> GetAllAsync(ServiceQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
                {
                    { "Id", queryParam.Id },
                    { "Name", queryParam.Name },
                };
            var servicesQuery = _dbContext.Services
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await servicesQuery;
        }

        public async Task<Service?> GetByIdAsync(int id, string? includes)
        {
            var servicesQuery = _dbContext.Services.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await servicesQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Service> CreateAsync(Service service)
        {
            _dbContext.Services.Add(service);
            await _dbContext.SaveChangesAsync();
            return service;
        }

        public async Task<Service> UpdateAsync(Service service)
        {
            _dbContext.Services.Update(service);
            await _dbContext.SaveChangesAsync();
            return service;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _dbContext.Services.FindAsync(id);
            if (service == null)
            {
                return false;
            }

            _dbContext.Services.Remove(service);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
