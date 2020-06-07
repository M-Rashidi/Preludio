using System;
using Autofac;

namespace Preludio.Config.Autofac
{
    internal class AutofacDependencyResolver : IDependencyResolver
    {
        
        private readonly ILifetimeScope _context;
        public AutofacDependencyResolver(ILifetimeScope context)
        {
            _context = context;
        }
        public T Resolve<T>()
        {
            return _context.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _context.Resolve(type);
        }
    }
}
