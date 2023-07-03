using SandAndStonesEngine.Assets;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Shaders;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameCamera;
using System.Diagnostics;

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
        private InputDevicesState inputDevicesState;
        private Camera gameCamera;
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

            inputDevicesState = new InputDevicesState();
            gameCamera = new Camera(gameGraphicDevice, inputDevicesState);
            gameCamera.InitMatricesShaderBinding();

            shaderBatch = new GameShaderSet(gameGraphicDevice, assets);
            shaderBatch.Create();
            gamePipeline = new GamePipeline(gameGraphicDevice, shaderBatch, gameCamera);
            gamePipeline.Create();
            gameCommandList = new GameCommandList(gameGraphicDevice, assets, gamePipeline);
            gameCommandList.Create();
        }

        public void Loop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double previousElapsedTime = sw.Elapsed.Seconds;
            gameCamera.DisplayMatrices();
            while (SDLWindow.Exists)
            {
                double newElapsedTime = sw.Elapsed.Seconds;
                double deltaElapsedTime = newElapsedTime - previousElapsedTime;
                var snapshot = SDLWindow.PumpEvents();
                inputDevicesState.Update(snapshot);

                previousElapsedTime = newElapsedTime;
                gameCamera.WindowResized(SDLWindow.Width, SDLWindow.Height);
                gameCamera.Update((float)deltaElapsedTime);
                Draw((float)deltaElapsedTime);
            }

            sw.Stop();
        }

        public void Stop()
        {
            DisposeResources();
        }

        private void Draw(float deltaTime)
        {
            gameCommandList.Draw(deltaTime);
        }

        private void DisposeResources()
        {
            gameCamera.Destroy();
            gamePipeline.Destroy();
            gameCommandList.Destroy();
            assets.Destroy();
            shaderBatch.Destroy();
            gameGraphicDevice.Destroy();
        }
    }
}
