using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Preludio.Domain.Model
{
    public interface IRepository
    {
        
    }
    public interface IRepository<TKey, T> : IRepository where T : IAggregateRoot
    {
        Task<TKey> GetNextId();
        void Create(T aggregate);
        void Remove(T aggregate);
        Task<T> Get(TKey key);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindAll(Expression<Func<T, bool>> predicate);
    }
}
