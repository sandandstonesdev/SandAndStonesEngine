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

namespace SandAndStonesEngine
{
    public class Game
    {
        readonly GameWindow window;
        public Game()
        {
            window = new GameWindow();
        }

        public void Start()
        {
            int x = 50;
            int y = 50;
            int screenWidth = 800;
            int screenHeight = 800;
            int quadCount = 10;
            var screenDivision = new ScreenDivisionForQuads(screenWidth, screenHeight, quadCount, quadCount);
            window.Start(x, y, screenWidth, screenHeight, "Sand and Stones Engine Test", screenDivision);
        }

        public void Loop()
        {
            window.Loop();
        }
        public void Stop()
        {
            window.Stop();
        }

    }
}
