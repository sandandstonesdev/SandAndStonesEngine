using SandAndStonesEngine.GraphicAbstractions;
using Veldrid;

namespace SandAndStonesEngine.GameFactories
{
    public sealed class Factory
    {
        private static readonly Lazy<Factory> lazyFactoryInstance = new Lazy<Factory>(() => new Factory());
        public static Factory Instance => lazyFactoryInstance.Value;
        private Factory()
        {
            
        }

        public Game GetGame()
        {
            return Game.Instance;
        }

        public GameGraphicDevice GetGameGraphicDevice()
        {
            var gameGraphicDevice = GameGraphicDevice.Instance;

            return gameGraphicDevice;
        }
        public ResourceFactory GetResourceFactory()
        {
            return GameGraphicDevice.Instance.ResourceFactory;
        }

        public GameWindow GetGameWindow()
        {
            return Game.Instance.Window;
        }
    }
}
