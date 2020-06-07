using Autofac;
using Preludio.Query;

namespace Preludio.Config.Autofac
{
    public class AutofacQueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly ILifetimeScope _lifetimeScope;
        public AutofacQueryHandlerFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        public IQueryHandler<TQuery, TResult> CreateHandler<TQuery, TResult>() where TQuery : IQuery<TResult>
        {
            return _lifetimeScope.Resolve<IQueryHandler<TQuery, TResult>>();
        }
    }
}
