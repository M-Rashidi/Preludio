namespace Preludio.Config.Loaders
{
    public class PreludioLoader : IIocModuleBuilder, IModuleBuilder
    {
        private PreludioLoader(){}
        public static IIocModuleBuilder Setup()
        {
            return new PreludioLoader(); 
        }
        public IModuleBuilder WithIocModule(IPreludioIocModule module)
        {
            PreludioModuleLoader.Install(module);
            return this;
        }

        public IModuleBuilder WithModule(IPreludioModule module)
        {
            PreludioModuleLoader.Install(module);
            return this;
        }

        public IModuleBuilder WithModule<T>() where T : IPreludioModule, new()
        {
            PreludioModuleLoader.Install<T>();
            return this;
        }
    }
}
