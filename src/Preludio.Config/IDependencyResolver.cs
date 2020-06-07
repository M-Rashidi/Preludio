using System;

namespace Preludio.Config
{
    public interface IDependencyResolver
    {
        T Resolve<T>();

        object Resolve(Type type);
    }
}
