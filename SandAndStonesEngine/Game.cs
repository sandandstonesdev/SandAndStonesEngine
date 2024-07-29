using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.ScreenDivisions;

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
            QuadGridManager.Instance.Init(screenDivision);
            Window.Start(x, y, screenWidth, screenHeight, "Sand and Stones Engine Test");
        }

        public void Loop()
        {
            Window.Loop();
        }
        public void Stop()
        {
            Window.Dispose();
        }

    }
}
