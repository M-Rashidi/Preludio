using Preludio.Core.EventHandling.EventHandlers;

namespace Preludio.Core.EventHandling
{
    public interface IEventAggregator
    {
        void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent;

        void Publish<TEvent>(TEvent eventToPublish) where TEvent : IEvent;
    }
}
