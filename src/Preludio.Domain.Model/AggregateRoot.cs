using System.Collections.Generic;
using Preludio.Core.EventHandling;

namespace Preludio.Domain.Model
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot
    {
        private IEventPublisher _publisher;
        private readonly List<IDomainEvent> _uncommittedEvents;

        protected AggregateRoot()
        {
            _uncommittedEvents = new List<IDomainEvent>();
        }

        protected AggregateRoot(IAggregateRootConfigurator configurator) : this()
        {
            configurator.Config(this);
        }

        public void SetPublisher(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Publish<TEvent>(TEvent eventToPublish) where TEvent : IDomainEvent
        {
            _publisher.Publish(eventToPublish);
            _uncommittedEvents.Add(eventToPublish);
        }

        public IReadOnlyList<IDomainEvent> GetUncommittedEvents()
        {
            return _uncommittedEvents.AsReadOnly();
        }

        public void ClearUncommittedEvents()
        {
            this._uncommittedEvents.Clear();
        }
    }
}