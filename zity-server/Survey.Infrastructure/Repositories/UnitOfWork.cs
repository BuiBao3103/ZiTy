using Survey.Domain.Core.Models;
using Survey.Domain.Core.Repositories;
using Survey.Infrastructure.Data;

namespace Survey.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly SurveyDbContext _dbContext;
        private readonly IDictionary<Type, dynamic> _repositories;
        private readonly IStatisticRepository _statisticRepository;


        public UnitOfWork(SurveyDbContext dbContext, IStatisticRepository statisticRepository)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, dynamic>();
            _statisticRepository = statisticRepository;

        }

        public IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity
        {
            var entityType = typeof(T);

            if (_repositories.ContainsKey(entityType))
            {
                return _repositories[entityType];
            }

            var repositoryType = typeof(BaseRepositoryAsync<>);

            var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

            if (repository == null)
            {
                throw new NullReferenceException("Repository should not be null");
            }

            _repositories.Add(entityType, repository);

            return (IBaseRepositoryAsync<T>)repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task RollBackChangesAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        public IStatisticRepository StatisticRepository => _statisticRepository;

    }
}