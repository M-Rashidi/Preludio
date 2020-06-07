using System.Collections.Generic;

namespace Preludio.Core.EventHandling
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        List<object> GetPublishedEvents();
        void ClearHistory();
    }
}
