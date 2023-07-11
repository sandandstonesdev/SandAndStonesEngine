using SandAndStonesEngine.GraphicAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.Sdl2;

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
