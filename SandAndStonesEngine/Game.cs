using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using Veldrid;
using System.Numerics;
using Veldrid.SPIRV;
using Vulkan;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameFactories;

namespace SandAndStonesEngine
{
    public class Game
    {
        private static readonly Lazy<Game> lazyFactoryInstance = new Lazy<Game>(() => new Game());
        public static Game Instance => lazyFactoryInstance.Value;

        public readonly GameWindow Window;
        public Game()
        {
            Window = GameWindow.Instance;
        }

        public void Start()
        {
            int x = 50;
            int y = 50;
            int screenWidth = 800;
            int screenHeight = 800;
            int quadCountX = 8;
            int quadCountY = 8;
            int quadCountZ = 8;
            var screenDivision = new ScreenDivisionForQuads(screenWidth, screenHeight, quadCountX, quadCountY, quadCountZ);
            Window.Start(x, y, screenWidth, screenHeight, "Sand and Stones Engine Test", screenDivision);
        }

        public void Loop()
        {
            Window.Loop();
        }
        public void Stop()
        {
            Window.Stop();
        }

    }
}
