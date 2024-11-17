using Apartment.Domain.Core.Models;

namespace Apartment.Domain.Core.Repositories;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
}
