using Preludio.Core.EventHandling.EventHandlers;

namespace Preludio.Core.EventHandling
{
    public class EventListener : IEventListener
    {
        private readonly IEventAggregator eventAggregator;
        public EventListener(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void Register<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IDomainEvent
        {
            eventAggregator.Subscribe(eventHandler);
        }
    }
}
