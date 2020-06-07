namespace Preludio.Core.EventHandling.EventHandlers
{
    public interface IEventHandler<in TEvent>
    {
        void Handle(TEvent eventToHandle);
    }
}
