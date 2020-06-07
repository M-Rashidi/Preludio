using System;
using System.Collections.Generic;

namespace Preludio.Core
{
    public interface IServiceLocator
    {
        T GetInstance<T>();
        object GetInstance(Type type);
        IEnumerable<T> GetAllInstances<T>();
        T GetInstanceWithKey<T>(string key);
        void Release(object service);
        bool HasInstance(Type type);
    }
}
