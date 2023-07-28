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
using SandAndStonesEngine.RenderingAbstractions;
using SandAndStonesEngine.GameTextures;
using Veldrid;

namespace SandAndStonesEngine
{
    public class GameWindow : IDisposable
    {
        private static readonly Lazy<GameWindow> lazyInstance = new Lazy<GameWindow>(() => new GameWindow());
        public static GameWindow Instance => lazyInstance.Value;

        public bool resized = true;
        private GameAssets assets;
        private GameStatusBarAssets statusBarAssets;
        private GameShaderSet shaderSet;
        public Sdl2Window SDLWindow;
        private GameCommandList gameCommandList;
        private GamePipeline gamePipeline;
        private StatusBarPipeline statusBarPipeline;
        private InputDevicesState inputDevicesState;
        private Camera gameCamera;
        private Matrices matrices;
        ScreenDivisionForQuads screenDivisionForQuads;
        public GameTextureSurface gameTextureSurface;

        ViewTransformator viewTransformator;
        WorldTransformator worldTransformator;
        CameraInputMotionMapper inputMotionMapper;

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
            var clientRegionPos =  new Vector2(SDLWindow.Bounds.X, SDLWindow.Bounds.Y);
            SDLWindow.Resized += () => resized = true;
            inputDevicesState = new InputDevicesState(clientRegionPos);
            inputMotionMapper = new CameraInputMotionMapper(inputDevicesState);
            var transformatorData = new TransformatorData(new Vector3(0, 0, 1.0f), new Vector3(0, 0, -1), new Vector3(0, 1, 0), new Vector3(0, 0, 0), 0.002f);
            this.viewTransformator = new ViewTransformator(inputMotionMapper, transformatorData);
            this.worldTransformator = new WorldTransformator(inputMotionMapper, transformatorData);

            matrices = new Matrices(worldTransformator, viewTransformator);
            matrices.Init();

            this.screenDivisionForQuads = screenDivisionForQuads;
            gameCamera = new Camera(matrices);

            gameTextureSurface = new GameTextureSurface(256, 256);
            gameTextureSurface.Init();

            assets = new GameAssets(gameTextureSurface, screenDivisionForQuads);
            assets.Create();
            shaderSet = new GameShaderSet(assets, matrices);
            shaderSet.Create();
            gamePipeline = new GamePipeline(shaderSet, assets, matrices);
            gamePipeline.Create();

            statusBarAssets = new GameStatusBarAssets(gameTextureSurface, screenDivisionForQuads);
            statusBarAssets.Create();
            statusBarPipeline = new StatusBarPipeline(shaderSet, statusBarAssets, matrices);
            statusBarPipeline.Create();

            gameCommandList = new GameCommandList(matrices, assets, statusBarAssets, gamePipeline, statusBarPipeline);
            gameCommandList.Init();
        }

        public void Loop()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            long newElapsedTime = Math.Max(0, sw.Elapsed.Milliseconds);
            long previousElapsedTime = 0;
            long deltaElapsedTime = newElapsedTime - previousElapsedTime;

            while (SDLWindow.Exists)
            {
                newElapsedTime = Math.Max(0, sw.ElapsedMilliseconds);
                deltaElapsedTime = newElapsedTime - previousElapsedTime;
                previousElapsedTime = newElapsedTime;

                var snapshot = SDLWindow.PumpEvents();
                inputDevicesState.Update(snapshot);
                viewTransformator.Update();
                worldTransformator.Update();

                if (resized)
                {
                    gameCamera.WindowResized(SDLWindow.Width, SDLWindow.Height);
                    screenDivisionForQuads.Resize(SDLWindow.Width, SDLWindow.Height);
                    resized = false;
                }

                gameCamera.Update(deltaElapsedTime);
                assets.Update(deltaElapsedTime);
                statusBarAssets.Update(deltaElapsedTime);

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
                statusBarPipeline.Dispose();
                gameCommandList.Dispose();
                var disposableAssets = assets as IDisposable;
                disposableAssets?.Dispose();
                var disposableStatusBarAssets = statusBarAssets as IDisposable;
                disposableStatusBarAssets?.Dispose();
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
