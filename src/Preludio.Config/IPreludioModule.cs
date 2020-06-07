namespace Preludio.Config
{
    public interface IPreludioModule
    {
        void Register(IServiceRegistry serviceRegistry);
    }
}
