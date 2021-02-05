using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Writely.Data;

namespace Writely.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity?> GetById(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public abstract Task<IEnumerable<TEntity>?> GetAll(Expression<Func<TEntity, bool>>? filter = null,
            string? order = null,
            int limit = 0);

        public abstract Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }
    }
}