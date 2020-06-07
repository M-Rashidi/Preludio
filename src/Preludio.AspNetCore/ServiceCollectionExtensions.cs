using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Preludio.Core;

namespace Preludio.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder SetupPreludioMvc(this IServiceCollection collection, Assembly restApiAssembly, Action<MvcOptions> optionConfigurator = null)
        {
            collection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            collection.TryAddSingleton<IUserResolver, HttpContextUserResolverAdapter>();

            return collection.AddMvc(options =>
                {
                    options.Conventions.Add(new CqsModelConvention());
                    optionConfigurator?.Invoke(options);
                })
            .AddApplicationPart(restApiAssembly);
        }
    }
}
