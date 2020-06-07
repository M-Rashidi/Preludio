using System.Threading.Tasks;

namespace Preludio.Query
{
    public class QueryBus : IQueryBus
    {
        private IQueryHandlerFactory _handlerFactory;
        public QueryBus(IQueryHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }
        public Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _handlerFactory.CreateHandler<TQuery, TResult>();
            return handler.Handle(query);
        }
    }
}
