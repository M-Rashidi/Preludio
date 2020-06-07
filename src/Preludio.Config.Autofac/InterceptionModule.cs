using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;

namespace Preludio.Config.Autofac
{
    public class InterceptionModule : Module
    {
        const string InterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName";
        private readonly Type _interceptorType;
        private readonly Type _typeToBeIntercepted;
        public InterceptionModule(Type interceptorType, Type typeToBeIntercepted)
        {
            _interceptorType = interceptorType;
            _typeToBeIntercepted = typeToBeIntercepted;
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            if (!ShouldIntercept(registration))
                return;

            var interceptorServices = new Service[] { new TypedService(_interceptorType) };

            object existing;
            if (registration.Metadata.TryGetValue(InterceptorsPropertyName, out existing))
            {
                registration.Metadata[InterceptorsPropertyName] =
                    ((IEnumerable<Service>)existing).Concat(interceptorServices).Distinct();
            }
            else
            {
                registration.Metadata.Add(InterceptorsPropertyName, interceptorServices);
            }
        }

        private bool ShouldIntercept(IComponentRegistration registration)
        {
            return this._typeToBeIntercepted.IsAssignableFrom(registration.Target.Activator.LimitType);
        }
    }
}
