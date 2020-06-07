namespace Preludio.Application.Contracts
{
    public interface ICommandHandlerFactory
    {
        CommandHandlerContainer<T> CreateHandlers<T>(T command);
    }
}
