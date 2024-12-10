using Domain.Core.Models;

namespace Domain.Core.Repositories;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task RollBackChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
    IStatisticRepository StatisticRepository { get; }
}
