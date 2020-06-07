using System.Collections.Generic;
using System.Linq;

namespace Preludio.Core.EventHandling
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly List<object> publishedEvents;
        public EventPublisher(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this.publishedEvents = new List<object>();
        }
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            _eventAggregator.Publish(@event);
            this.publishedEvents.Add(@event);
        }
        public List<object> GetPublishedEvents()
        {
            return publishedEvents.ToList();
        }
        public void ClearHistory()
        {
            this.publishedEvents.Clear();
        }
    }
}
