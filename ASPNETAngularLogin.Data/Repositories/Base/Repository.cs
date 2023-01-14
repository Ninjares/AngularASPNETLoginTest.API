using ASPNETAngularLogin.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETAngularLogin.Data.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected ASPNETAngularLoginDbContext _context;

        public Repository(ASPNETAngularLoginDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity t)
        {
            _context.Set<TEntity>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<ICollection<TEntity>> AddManyAsync(ICollection<TEntity> t)
        {
            await _context.Set<TEntity>().AddRangeAsync(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity t)
        {
            _context.Set<TEntity>().Update(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public virtual async Task<ICollection<TEntity>> UpdateManyAsync(ICollection<TEntity> t)
        {
            _context.Set<TEntity>().UpdateRange(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteManyAsync(ICollection<TEntity> t)
        {
            _context.Set<TEntity>().RemoveRange(t);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetAsync(object key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            var res = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            return res;
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, bool noTracking = true)
        {
            if (noTracking)
            {
                return await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(match);
            }
            else
            {
                return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
            }
        }

        public virtual async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
