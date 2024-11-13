using Microsoft.EntityFrameworkCore;
using Report.Domain.Core.Specifications;
using Report.Domain.Core.Models;
using Report.Domain.Core.Repositories;
using Report.Infrastructure.Data;

namespace Report.Infrastructure.Repositories;
public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
{
    protected readonly ReportDbContext _dbContext;

    public BaseRepositoryAsync(ReportDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync(object id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    }
}
