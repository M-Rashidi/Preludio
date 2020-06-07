using Preludio.Core.EventHandling.EventHandlers;

namespace Preludio.Core.EventHandling
{
    public interface IEventListener
    {
        void Register<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IDomainEvent;
    }
}
