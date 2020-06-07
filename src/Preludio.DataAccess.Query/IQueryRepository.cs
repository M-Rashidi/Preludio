using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Preludio.DataAccess.Query
{
    public interface IQueryRepository
    {
    }

    public interface IQueryRepository<in TKey, T> : IQueryRepository
    {
        Task<T> Get(TKey id);
        Task<T1> Get<T1>(TKey id) where T1 : class, T;
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindAll(Expression<Func<T, bool>> predicate);
        Task<T1> Find<T1>(Expression<Func<T1, bool>> predicate) where T1 : class, T;
        Task<List<T1>> FindAll<T1>(Expression<Func<T1, bool>> predicate) where T1 : class, T;
    }
}
