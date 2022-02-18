using System.Threading.Tasks;
using EventStore.ClientAPI;
using Preludio.Domain.Model;

namespace Preludio.DataAccess.EventStore
{
    public abstract class EventSourcedRepository<TKey, TAggregate> where TAggregate : EventSourcedAggregateRoot<TKey>
    {
        private readonly IEventStoreConnection _connection;
        private readonly IAggregateRootConfigurator _configurator;

        protected EventSourcedRepository(IEventStoreConnection connection, IAggregateRootConfigurator configurator)
        {
            _connection = connection;
            _configurator = configurator;
        }
        public async Task<TAggregate> GetById(TKey id)
        {
            var stream = GetStreamName(id);
            var streamEvents = await _connection.ReadStreamEventsForwardAsync(stream, 0, 4096, false);
            if (streamEvents.Status != SliceReadStatus.Success) return null;
            var domainEvents = DomainEventFactory.Create(streamEvents.Events);
            return AggregateFactory.Create<TAggregate, TKey>(domainEvents, _configurator);
        }

        public async Task Append(TAggregate aggregate)
        {
            var changes = aggregate.GetUncommittedEvents();
            var eventData = EventDataFactory.Create(changes);
            var streamName = GetStreamName(aggregate.Id);
            await _connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData);
        }

        private string GetStreamName(TKey id)
        {
            return $"{GetAggregateName()}-{id.ToString()}";
        }

        protected abstract string GetAggregateName();
    }
}
