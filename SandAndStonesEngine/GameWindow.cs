using SandAndStonesEngine.Assets;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Shaders;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace SandAndStonesEngine
{
    public class GameWindow
    {

        private GameAssets assets;
        private GameGraphicDevice gameGraphicDevice;
        private GameShaderSet shaderBatch;
        public Sdl2Window SDLWindow;
        private GameCommandList gameCommandList;
        private GamePipeline gamePipeline;

        public GameWindow()
        {
            
        }

        public void Start(int x, int y, int width, int height, string title, ScreenDivisionForQuads screenDivisionForQuads)
        {
            WindowCreateInfo windowCI = new()
            {
                X = x,
                Y = y,
                WindowWidth = width,
                WindowHeight = height,
                WindowTitle = title,
            };
            SDLWindow = VeldridStartup.CreateWindow(ref windowCI);

            gameGraphicDevice = new GameGraphicDevice(this);
            gameGraphicDevice.Create();
            assets = new GameAssets(gameGraphicDevice, screenDivisionForQuads);
            assets.Create();

            shaderBatch = new GameShaderSet(gameGraphicDevice, assets);
            shaderBatch.Create();
            gamePipeline = new GamePipeline(gameGraphicDevice, shaderBatch);
            gamePipeline.Create();
            gameCommandList = new GameCommandList(gameGraphicDevice, assets, gamePipeline);
            gameCommandList.Create();
        }

        public void Loop()
        {
            while (SDLWindow.Exists)
            {
                SDLWindow.PumpEvents();
                Draw();
            }
        }

        public void Stop()
        {
            DisposeResources();
        }

        private void Draw()
        {
            gameCommandList.Draw();
        }

        private void DisposeResources()
        {
            gamePipeline.Destroy();
            gameCommandList.Destroy();
            assets.Destroy();
            shaderBatch.Destroy();
            gameGraphicDevice.Destroy();
        }
    }
}
