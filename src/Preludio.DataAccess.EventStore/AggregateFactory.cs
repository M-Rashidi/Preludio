using System;
using System.Collections.Generic;
using Preludio.Core.EventHandling;
using Preludio.Domain.Model;

namespace Preludio.DataAccess.EventStore
{
    public static class AggregateFactory
    {
        public static T Create<T, TKey>(List<DomainEvent> domainEvents, IAggregateRootConfigurator configurator) where T : EventSourcedAggregateRoot<TKey>
        {
            var entity = (T)Activator.CreateInstance(typeof(T), true);
            domainEvents.ForEach(a => entity.Apply(a));
            entity = configurator.Config(entity);
            return entity;
        }
    }
}
