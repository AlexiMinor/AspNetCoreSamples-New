﻿using System.Linq.Expressions;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.Core.Interfaces.Data;

public interface IRepository<T> where T : BaseEntity
{
    public Task Add(T obj);
    public Task AddRange(IEnumerable<T> obj);

    public Task<T> GetById(Guid id);
    public Task<T> GetByIdWithIncludes(Guid id,
        params Expression<Func<T, object>>[] includes);

    public IQueryable<T> Get();

    public Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);

    public Task Update(T obj);

    public Task PatchAsync(Guid id, List<PatchModel> patchDtos);

    public Task Remove(Guid id);

    public void Dispose();
}