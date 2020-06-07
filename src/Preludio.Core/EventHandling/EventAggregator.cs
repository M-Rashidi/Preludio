using System.Collections.Generic;
using System.Linq;
using Preludio.Core.EventHandling.EventHandlers;

namespace Preludio.Core.EventHandling
{
    public class EventAggregator : IEventAggregator
    {
        private readonly List<object> _subscriberList = new List<object>(); 
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            _subscriberList.Add(eventHandler);
        }
        public void Publish<TEvent>(TEvent eventToPublish) where TEvent : IEvent
        {
            var list = _subscriberList.Where(a => a is IEventHandler<TEvent>).OfType<IEventHandler<TEvent>>().ToList();
            foreach (var eventHandler in list)
            {
                eventHandler.Handle(eventToPublish);
            }
        }
    }
}
