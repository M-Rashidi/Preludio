namespace Preludio.Config.Loaders
{
    public interface IIocModuleBuilder
    {
        IModuleBuilder WithIocModule(IPreludioIocModule module);
    }
}