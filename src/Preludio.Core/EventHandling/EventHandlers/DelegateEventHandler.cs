using System;

namespace Preludio.Core.EventHandling.EventHandlers
{
    public class DelegateEventHandler<TEvent> : IEventHandler<TEvent>
    {
        private readonly Action<TEvent> action;
        public DelegateEventHandler(Action<TEvent> action)
        {
            this.action = action;
        }

        public void Handle(TEvent eventToHandle)
        {
            action(eventToHandle);
        }
    }
}
