using Report.Domain.Core.Models;

namespace Report.Domain.Core.Repositories;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
}
