using System.Collections.Generic;
using Preludio.Core.EventHandling;

namespace Preludio.Domain.Model
{
    public interface IAggregateRoot
    {
        void SetPublisher(IEventPublisher publisher);
        void Publish<TEvent>(TEvent eventToPublish) where TEvent : IDomainEvent;
        IReadOnlyList<IDomainEvent> GetUncommittedEvents();
    }
}
