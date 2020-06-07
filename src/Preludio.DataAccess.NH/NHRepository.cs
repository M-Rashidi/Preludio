using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Preludio.Domain.Model;

namespace Preludio.DataAccess.NH
{
    public abstract class NHRepository<TKey,T> : IRepository<TKey, T> where T : IAggregateRoot
    {
        protected IAggregateRootConfigurator Configurator { get; private set; }
        protected SequenceHelper Sequence { get; private set; }
        protected ISession Session { get; private set; }
        protected NHRepository(ISession session, IAggregateRootConfigurator configurator)
        {
            this.Configurator = configurator;
            this.Session = session;
            this.Sequence = new SequenceHelper(session);
        }

        public abstract Task<TKey> GetNextId();

        public void Create(T entity)
        {
            Session.Save(entity);
        }
        public void Remove(T entity)
        {
            Session.Delete(entity);
        }
        public async Task<T> Get(TKey key)
        {
            var value = await Session.GetAsync<T>(key);
            return Configurator.Config(value);
        }
        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            var values = await Session.Query<T>().Where(predicate).FirstOrDefaultAsync();
            return Configurator.Config(values);
        }
        public async Task<List<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            var values = await Session.Query<T>().Where(predicate).ToListAsync();
            return values.Select(Configurator.Config).ToList();
        }
    }
}
