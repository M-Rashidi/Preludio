namespace Preludio.Config.Loaders
{
    public interface IModuleBuilder
    {
        IModuleBuilder WithModule(IPreludioModule module);
        IModuleBuilder WithModule<T>() where T : IPreludioModule, new();

    }
}