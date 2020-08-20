using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logstore_BackEnd.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> Get();
        Task<IList<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}
