namespace Preludio.Config
{
    public interface IPreludioIocModule : IPreludioModule
    {
        IServiceRegistry CreateServiceRegistry();
    }
}