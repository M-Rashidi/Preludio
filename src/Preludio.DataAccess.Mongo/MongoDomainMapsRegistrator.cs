using System;
using System.Linq;
using System.Reflection;
using Preludio.DataAccess.Mongo.DocumentMaps;

namespace Preludio.DataAccess.Mongo
{
    public static class MongoDomainMapsRegistrator
    {
        public static void RegisterDocumentMaps(Assembly assembly)
        {            
            var classMaps = assembly.GetTypes()
            .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
              t.BaseType.GetGenericTypeDefinition() == typeof(MongoDbClassMap<>));
            
            foreach (var classMap in classMaps)
                Activator.CreateInstance(classMap);
        }
    }
}
