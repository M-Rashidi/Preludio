namespace Preludio.Config
{
    internal static class PreludioModuleLoader
    {
        public static void Install<T>(T module) where T : IPreludioModule
        {
            if (module is IPreludioIocModule ioc) ServiceRegistry.SetCurrent(ioc.CreateServiceRegistry());

            var serviceRegistry = ServiceRegistry.Current;
            module.Register(serviceRegistry);
        }

        public static void Install<T>() where T : IPreludioModule, new()
        {
            var module = new T();
            Install(module);
        }
    }
}
