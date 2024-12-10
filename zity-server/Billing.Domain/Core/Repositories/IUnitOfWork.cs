using Billing.Domain.Core.Models;

namespace Billing.Domain.Core.Repositories;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
    IStatisticRepository StatisticRepository { get; }
}
