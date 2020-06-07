using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Event;
using Preludio.Domain.Model;

namespace Preludio.DataAccess.NH.DomainEventPersistence
{
    public class DomainEventPersistListener : IPreDeleteEventListener,
                                              IPreInsertEventListener,
                                              IPreUpdateEventListener
    {
        private readonly IDomainEventCommandBuilder _builder;
        public DomainEventPersistListener(IDomainEventCommandBuilder builder)
        {
            _builder = builder;
        }
        public async Task<bool> OnPreDeleteAsync(PreDeleteEvent @event, CancellationToken cancellationToken)
        {
            return await HandleAsync(@event.Session, @event.Entity);
        }

        public bool OnPreDelete(PreDeleteEvent @event)
        {
            return Handle(@event.Session, @event.Entity);
        }

        public async Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return await HandleAsync(@event.Session, @event.Entity);
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            return Handle(@event.Session, @event.Entity);
        }

        public async Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return await HandleAsync(@event.Session, @event.Entity);
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            return Handle(@event.Session, @event.Entity);
        }

        private bool Handle(ISession eventSession, object entity)
        {
            var aggregateRoot = entity as IAggregateRoot;
            if (aggregateRoot == null) return false;

            foreach (var uncommittedEvent in aggregateRoot.GetUncommittedEvents())
            {
                var command = _builder.Build(uncommittedEvent);
                command.Connection = eventSession.Connection as SqlConnection;
                eventSession.Transaction.Enlist(command);
                command.ExecuteNonQuery();
            }
            return false;
        }
        private async Task<bool> HandleAsync(ISession eventSession, object entity)
        {
            var aggregateRoot = entity as IAggregateRoot;
            if (aggregateRoot == null) return false;

            foreach (var uncommittedEvent in aggregateRoot.GetUncommittedEvents())
            {
                var command = _builder.Build(uncommittedEvent);
                command.Connection = eventSession.Connection as SqlConnection;
                eventSession.Transaction.Enlist(command);
                await command.ExecuteNonQueryAsync();
            }
            return false;
        }
    }
}
