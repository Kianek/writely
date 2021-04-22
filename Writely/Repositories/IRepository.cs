using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetById(long id);
        Task<IEnumerable<TEntity>?> GetAll(QueryFilter? filter = null);
        Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}