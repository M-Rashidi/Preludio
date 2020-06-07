namespace Preludio.Domain.Model
{
    public interface IAggregateRootConfigurator
    {
        T Config<T>(T aggregateRoot) where T : IAggregateRoot;
    }
}
