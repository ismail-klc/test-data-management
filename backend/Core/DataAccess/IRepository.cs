using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        List<T> Get(Expression<Func<T, bool>> filter = null);
        T GetByFilter(Expression<Func<T, bool>> filter);
        T Add(T entity);
        bool AddRange(IEnumerable<T> entities);
        T Update(T entity, Expression<Func<T, bool>> filter);
        T Delete(T entity);
        T Delete(Expression<Func<T, bool>> filter);
    }
}
