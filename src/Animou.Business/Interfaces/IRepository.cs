﻿using Animou.Business.Models;
using System.Linq.Expressions;

namespace Animou.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task <IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> Save();
    }
}
