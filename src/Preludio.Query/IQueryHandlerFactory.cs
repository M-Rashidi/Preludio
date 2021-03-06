namespace Preludio.Query
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TResult> CreateHandler<TQuery, TResult>() where TQuery : IQuery<TResult>;
    }
}
