namespace Preludio.Config
{
    public static class ServiceRegistry
    {
        public static IServiceRegistry Current { get; private set; }
        public static void SetCurrent(IServiceRegistry registry)
        {
            Current = registry;
        }
    }
}
