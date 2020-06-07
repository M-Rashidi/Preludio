using System;

namespace Preludio.Core.EventHandling
{
    public interface IDomainEvent : IEvent
    {
        Guid EventId { get; }
        DateTime PublishDateTime { get; }
    }
}
