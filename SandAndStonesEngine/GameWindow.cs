using SandAndStonesEngine.Assets;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Shaders;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameCamera;
using System.Diagnostics;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.GameFactories;
using System.Numerics;

namespace SandAndStonesEngine
{
    public class GameWindow
    {
        private static readonly Lazy<GameWindow> lazyInstance = new Lazy<GameWindow>(() => new GameWindow());
        public static GameWindow Instance => lazyInstance.Value;

        private GameAssets assets;
        private GameGraphicDevice gameGraphicDevice;
        private GameShaderSet shaderSet;
        public Sdl2Window SDLWindow;
        private GameCommandList gameCommandList;
        private GamePipeline gamePipeline;
        private InputDevicesState inputDevicesState;
        private Camera gameCamera;
        private Matrices matrices;
        ScreenDivisionForQuads screenDivisionForQuads;
        private GameWindow()
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

            matrices = new Matrices();
            matrices.Create();
            inputDevicesState = new InputDevicesState();

            this.screenDivisionForQuads = screenDivisionForQuads;
            var transformatorData = new TransformatorData(new Vector3(0, 0, 1.0f), new Vector3(0, 0, -1), new Vector3(0, 1, 0), new Vector2(0, 0), 0.002f);
            gameCamera = new Camera(inputDevicesState, matrices, transformatorData);
            assets = new GameAssets(screenDivisionForQuads, matrices, inputDevicesState, transformatorData);
            assets.Create();

            shaderSet = new GameShaderSet(assets, matrices);
            shaderSet.Create();
            gamePipeline = new GamePipeline(shaderSet, assets, matrices);
            gamePipeline.Create();
            gameCommandList = new GameCommandList(assets, gamePipeline);
            gameCommandList.Create();
        }

        public void Loop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double previousElapsedTime = sw.Elapsed.Seconds;
            
            while (SDLWindow.Exists)
            {
                double newElapsedTime = sw.Elapsed.Seconds;
                double deltaElapsedTime = newElapsedTime - previousElapsedTime;
                var snapshot = SDLWindow.PumpEvents();
                inputDevicesState.Update(snapshot);

                previousElapsedTime = newElapsedTime;
                gameCamera.WindowResized(SDLWindow.Width, SDLWindow.Height);
                screenDivisionForQuads.Resize(SDLWindow.Width, SDLWindow.Height);
                gameCamera.Update((float)deltaElapsedTime);

                assets.Update(deltaElapsedTime);

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
            matrices.Destroy();
            gameCamera.Destroy();
            gamePipeline.Destroy();
            gameCommandList.Destroy();
            assets.Destroy();
            shaderSet.Destroy();
            //gameGraphicDevice.Destroy();
        }
    }
}
