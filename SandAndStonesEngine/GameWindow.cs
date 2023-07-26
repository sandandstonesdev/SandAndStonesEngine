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
    public class GameWindow : IDisposable
    {
        private static readonly Lazy<GameWindow> lazyInstance = new Lazy<GameWindow>(() => new GameWindow());
        public static GameWindow Instance => lazyInstance.Value;

        private GameAssets assets;
        private GameShaderSet shaderSet;
        public Sdl2Window SDLWindow;
        private GameCommandList gameCommandList;
        private GamePipeline gamePipeline;
        private InputDevicesState inputDevicesState;
        private Camera gameCamera;
        private Matrices matrices;
        ScreenDivisionForQuads screenDivisionForQuads;
        private bool disposedValue;

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
            gameCommandList.Init();
        }

        public void Loop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            long deltaElapsedTime = 0;
            long newElapsedTime = sw.Elapsed.Milliseconds;
            long previousElapsedTime = 0;
            
            while (SDLWindow.Exists)
            {
                newElapsedTime = Math.Max(0, sw.ElapsedMilliseconds);
                deltaElapsedTime = newElapsedTime - previousElapsedTime;
                previousElapsedTime = newElapsedTime;

                var snapshot = SDLWindow.PumpEvents();
                inputDevicesState.Update(snapshot);

                gameCamera.WindowResized(SDLWindow.Width, SDLWindow.Height);
                screenDivisionForQuads.Resize(SDLWindow.Width, SDLWindow.Height);
                gameCamera.Update((float)deltaElapsedTime);

                assets.Update(deltaElapsedTime);

                Draw((float)deltaElapsedTime);
            }

            sw.Stop();
        }

        private void Draw(float deltaTime)
        {
            gameCommandList.Draw(deltaTime);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                matrices.Dispose();
                gameCamera.Dispose();
                gamePipeline.Dispose();
                gameCommandList.Dispose();
                var disposableAssets = assets as IDisposable;
                disposableAssets?.Dispose();
                shaderSet?.Dispose();

                disposedValue = true;
            }
        }

        ~GameWindow()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
