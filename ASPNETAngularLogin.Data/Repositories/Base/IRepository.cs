using ASPNETAngularLogin.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETAngularLogin.Data.Repositories.Base
{
    public interface IRepository<T>
        where T : IBaseEntity
    {
        Task<T> AddAsync(T t);

        Task<ICollection<T>> AddManyAsync(ICollection<T> t);

        Task<T> UpdateAsync(T t);

        Task<ICollection<T>> UpdateManyAsync(ICollection<T> t);

        Task DeleteAsync(T entity);

        Task DeleteManyAsync(ICollection<T> t);

        Task<T> GetAsync(object key);

        IQueryable<T> GetAll();

        Task<ICollection<T>> GetAllAsync();

        Task<T> FindAsync(Expression<Func<T, bool>> match, bool noTracking = true);

        Task<int> SaveChanges();
    }
}

