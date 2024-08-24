namespace SandAndStonesEngine.GameFactories
{
    public sealed class Factory
    {
        private static readonly Lazy<Factory> lazyFactoryInstance = new(() => new Factory());
        public static Factory Instance => lazyFactoryInstance.Value;
        private Factory()
        {

        }

        public Game GetGame()
        {
            return Game.Instance;
        }
    }
}
