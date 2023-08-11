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
using System.Numerics;
using SandAndStonesEngine.RenderingAbstractions;
using SandAndStonesEngine.GameTextures;

namespace SandAndStonesEngine
{
    public class GameWindow : IDisposable
    {
        private static readonly Lazy<GameWindow> lazyInstance = new Lazy<GameWindow>(() => new GameWindow());
        public static GameWindow Instance => lazyInstance.Value;

        public bool resized = true;
        List<GameAssetBatchBase> assetBatchList;
        private GameShaderSet shaderSet;
        public Sdl2Window SDLWindow;
        private GameCommandList gameCommandList;
        private List<PipelineBase> pipelineList;
        private InputDevicesState inputDevicesState;
        private Camera gameCamera;
        private Matrices matrices;
        public GameTextureSurface gameTextureSurface;

        ViewTransformator viewTransformator;
        WorldTransformator worldTransformator;
        CameraInputMotionMapper inputMotionMapper;

        private bool disposedValue;

        private GameWindow()
        {
            
        }

        public void Start(int x, int y, int width, int height, string title)
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

            gameCamera = new Camera(matrices);

            assetBatchList = new List<GameAssetBatchBase>
            {
                new GameAssetBatch(viewTransformator),
                new GameStatusBarAssetBatch()
            };
            assetBatchList.ForEach(e => e.Init());

            shaderSet = new GameShaderSet(assetBatchList[0], matrices);
            shaderSet.Init();

            List<GameAssetBase> gameAssets = new List<GameAssetBase>();
            assetBatchList.ForEach(e => gameAssets.AddRange(e.Assets));
            gameTextureSurface = new GameTextureSurface(gameAssets, 256, 256);
            gameTextureSurface.Init();

            pipelineList = new List<PipelineBase>()
            {
                new GamePipeline(shaderSet, gameTextureSurface, matrices),
                new StatusBarPipeline(shaderSet, gameTextureSurface, matrices)
            };
            pipelineList.ForEach(e => e.Init());

            gameCommandList = new GameCommandList(matrices, gameTextureSurface, assetBatchList, pipelineList);
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
                if (SDLWindow.Exists)
                {
                    viewTransformator.Update();
                    worldTransformator.Update();

                    if (resized)
                    {
                        gameCamera.WindowResized(SDLWindow.Width, SDLWindow.Height);
                        QuadGridManager.Instance.Resize(SDLWindow.Width, SDLWindow.Height);
                        resized = false;
                    }

                    gameCamera.Update(deltaElapsedTime);
                    assetBatchList.ForEach(e => e.Update(deltaElapsedTime));
                    gameTextureSurface.Update();

                    Draw((float)deltaElapsedTime);
                }
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
                pipelineList.ForEach(e=> e.Dispose());
                gameCommandList.Dispose();

                assetBatchList.ForEach(e => e.Dispose());
                shaderSet.Dispose();
                gameTextureSurface.Dispose();
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
