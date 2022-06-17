using Animou.Business.Interfaces;
using Animou.Business.Models;
using Animou.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Animou.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AnimouContext Database;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(AnimouContext database)
        {
            Database = database;
            DbSet = database.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await Save();
        }

        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await Save();
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await Save();
        }

        public async Task<int> Save()
        {
            return await Database.SaveChangesAsync();
        }

        public void Dispose()
        {
            Database?.Dispose();
        }
    }
}
