﻿using Domain.Core.Models;
using Domain.Core.Specifications;

namespace Domain.Core.Repositories;
public interface IBaseRepositoryAsync<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(object id);
    Task<IList<T>> ListAllAsync();
    Task<IList<T>> ListAsync(ISpecification<T> spec);
    Task<T?> FirstOrDefaultAsync(ISpecification<T?> spec);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
}
