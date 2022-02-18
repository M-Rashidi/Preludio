using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Preludio.Application.Contracts;
using Preludio.Core;
using Preludio.DataAccess.Query;
using Preludio.Domain.Model;
using Preludio.Query;

namespace Preludio.Config.Autofac
{
    internal class AutofacServiceRegistry : IServiceRegistry
    {
        private readonly ContainerBuilder container;
        public AutofacServiceRegistry(ContainerBuilder container)
        {
            this.container = container;
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            container.RegisterType<TImplementation>().As<TService>().SingleInstance();
        }
        public void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class
                                                                            where TInstance : TService
        {
            container.RegisterInstance<TService>(instance).SingleInstance();
        }
        public void RegisterTransient<TService, TImplementation>() where TImplementation : TService
        {
            container.RegisterType<TImplementation>().As<TService>().InstancePerDependency();
        }

        public void RegisterDecorator<TService, TDecorator>() where TDecorator : TService
        {
            container.RegisterDecorator<TDecorator, TService>();
        }

        public void RegisterDecorator(Type service, Type decorator)
        {
            container.RegisterGenericDecorator(decorator, service, "commandHandler");
        }

        public void RegisterScoped<TService>(Func<TService> factory, Action<TService> release = null)
        {
            var registration = container.Register(a => factory.Invoke());
            if (release != null)
                registration.OnRelease(release);

            registration.InstancePerLifetimeScope();
        }

        public void RegisterScoped<TService>(Type implementationType)
        {
            container.RegisterType(implementationType).As<TService>().InstancePerLifetimeScope();
        }

        public void RegisterScoped(Type implementationType)
        {
            container.RegisterType(implementationType).InstancePerLifetimeScope();
        }

        public void RegisterScoped<TService>(Func<IDependencyResolver, TService> factory, Action<TService> release = null)
        {
            var registration = container.Register((context) => 
                factory.Invoke(new AutofacDependencyResolver(context.Resolve<ILifetimeScope>())));
            if (release != null)
                registration.OnRelease(release);
            registration.InstancePerLifetimeScope();
        }

        public void RegisterScoped<TService, TImplementation>() where TImplementation : TService
        {
            container.RegisterType<TImplementation>().As<TService>().InstancePerLifetimeScope();
        }

        public void RegisterNamed<TService>(Func<TService> factory,string name, Action<TService> release = null)
        {
            var registration = container.Register(a => factory.Invoke()).Named<TService>(name)
                .InstancePerLifetimeScope();
            if (release != null)
                registration.OnRelease(release);
        }
        public void RegisterNamed<TService>(Func<IDependencyResolver, TService> factory, string name, Action<TService> release = null)
        {
            var registration = container.Register(a => 
                    factory.Invoke(new AutofacDependencyResolver(a.Resolve<ILifetimeScope>())))
                .Named<TService>(name)
                .InstancePerLifetimeScope();
            if (release != null)
                registration.OnRelease(release);
        }

        public void RegisterCommandHandlers(Assembly assembly)
        {
            container.RegisterAssemblyTypes(assembly)
                .As(type => type.GetInterfaces()
                .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>)))
                .Select(interfaceType => new KeyedService("commandHandler", interfaceType)));
        }

        public void RegisterQueryHandlers(Assembly assembly)
        {
            container.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        public void RegisterRepositories(Assembly assembly)
        {
            container.RegisterAssemblyTypes(assembly)
                .Where(a => typeof(IRepository).IsAssignableFrom(a))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        public void RegisterDomainServices(Assembly assembly)
        {
            container.RegisterAssemblyTypes(assembly)
                .Where(a => typeof(IDomainService).IsAssignableFrom(a))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        public void RegisterFacades(Assembly assembly)
        {
            container.RegisterAssemblyTypes(assembly)
                .Where(a => typeof(IFacadeService).IsAssignableFrom(a))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InstancePerLifetimeScope();
        }
        public void RegisterQueryRepositories(Assembly assembly)
        {
            container.RegisterAssemblyTypes(assembly)
                .Where(a=>typeof(IQueryRepository).IsAssignableFrom(a))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

    }
}