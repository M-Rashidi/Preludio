using Preludio.Core.EventHandling;

namespace Preludio.Domain.Model
{
    public class AggregateRootConfigurator : IAggregateRootConfigurator
    {
        private readonly IEventPublisher publisher;

        public AggregateRootConfigurator(IEventPublisher publisher)
        {
            this.publisher = publisher;
        }

        public T Config<T>(T aggregateRoot) where T : IAggregateRoot
        {
            if (aggregateRoot != null)
                aggregateRoot.SetPublisher(publisher);
            return aggregateRoot;
        }
    }
}
