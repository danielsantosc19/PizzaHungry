using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logstore_BackEnd.Model;

namespace Logstore_BackEnd.Repository
{
    public abstract class RepositoryBase<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        protected RepositoryBase(DbContext context)
        {
            _context = context;
        }
        public virtual async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            _context.SaveChanges();
        }
        public virtual async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
        public virtual async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<IList<TEntity>> Get()
        {
            return await _context.Set<TEntity>().ToListAsync<TEntity>();
        }
        public virtual async Task<IList<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public virtual async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
