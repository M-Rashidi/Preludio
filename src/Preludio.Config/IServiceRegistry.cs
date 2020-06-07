using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace Preludio.Config
{
    public interface IServiceRegistry
    {
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
        void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService;
        void RegisterTransient<TService, TImplementation>() where TImplementation : TService;
        void RegisterDecorator<TService, TDecorator>() where TDecorator : TService;
        void RegisterDecorator(Type service, Type decorator);
        void RegisterScoped<TService>(Func<TService> factory, Action<TService> release = null);
        void RegisterScoped<TService>(Type implementationType);
        void RegisterScoped(Type implementationType);
        void RegisterScoped<TService>(Func<IDependencyResolver, TService> factory, Action<TService> release = null);
        void RegisterScoped<TService, TImplementation>() where TImplementation : TService;
        void RegisterCommandHandlers(Assembly assembly);
        void RegisterRepositories(Assembly assembly);
        void RegisterFacades(Assembly assembly);
        void RegisterQueryHandlers(Assembly assembly);
        void RegisterDomainServices(Assembly assembly);
        void RegisterQueryRepositories(Assembly assembly);
        void RegisterNamed<TService>(Func<TService> factory, string name, Action<TService> release = null);
        void RegisterNamed<TService>(Func<IDependencyResolver, TService> factory, string name, Action<TService> release = null);
        void SetInterceptorOnType<TInterceptor, TType>() where TInterceptor : IInterceptor;
    }
}
