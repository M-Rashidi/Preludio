using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Preludio.Core.EventHandling;

namespace Preludio.DataAccess.EventStore
{
    internal static class EventDataFactory
    {
        public static EventData Create(IDomainEvent @event)
        {
            var type = @event.GetType().AssemblyQualifiedName;
            var serialized = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            return new EventData(@event.EventId, type, true, serialized, null);
        }

        public static List<EventData> Create(IEnumerable<IDomainEvent> events)
        {
            return events.Select(Create).ToList();
        }
    }
}
