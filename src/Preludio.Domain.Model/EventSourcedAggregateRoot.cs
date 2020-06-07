using Preludio.Core.EventHandling;

namespace Preludio.Domain.Model
{
    public abstract class EventSourcedAggregateRoot<TKey> : AggregateRoot<TKey>
    {
        public abstract void Apply(DomainEvent @event);
        protected EventSourcedAggregateRoot(){}
        protected EventSourcedAggregateRoot(IAggregateRootConfigurator configurator) : base(configurator)
        {
            
        }
    }
}
