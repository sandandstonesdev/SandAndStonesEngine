using Microsoft.Extensions.DependencyInjection;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Shaders;
using System.Diagnostics;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace SandAndStonesEngine
{
    public class GameWindow : IDisposable
    {
        public bool resized = true;
        public Sdl2Window SDLWindow;

        private bool disposedValue;

        public GameWindow()
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
            SDLWindow.Resized += () => resized = true;
        }

        public void Loop()
        {
            var inputDevicesState = Startup.ServiceProvider.GetRequiredService<InputDevicesState>();
            var viewTransformator = Startup.ServiceProvider.GetRequiredService<ViewTransformator>();
            var worldTransformator = Startup.ServiceProvider.GetRequiredService<WorldTransformator>();
            var gameCamera = Startup.ServiceProvider.GetRequiredService<Camera>();
            var gridManager = Startup.ServiceProvider.GetRequiredService<QuadGridManager>();
            var gameTextureSurface = Startup.ServiceProvider.GetRequiredService<GameTextureSurface>();
            var gameCommandList = Startup.ServiceProvider.GetRequiredService<GameCommandList>();

            var sw = new Stopwatch();
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
                    viewTransformator.Update(deltaElapsedTime);
                    worldTransformator.Update(deltaElapsedTime);

                    if (resized)
                    {
                        gameCamera.WindowResized(SDLWindow.Width, SDLWindow.Height);
                        gridManager.Resize(SDLWindow.Width, SDLWindow.Height);
                        resized = false;
                    }

                    gameCamera.Update(deltaElapsedTime);
                    gameCommandList.assetBatchList.ForEach(e => e.Update(deltaElapsedTime));
                    gameTextureSurface.Update();

                    Draw((float)deltaElapsedTime);
                }
            }

            sw.Stop();
        }

        private void Draw(float deltaTime)
        {
            var gameCommandList = Startup.ServiceProvider.GetRequiredService<GameCommandList>();
            gameCommandList.Draw(deltaTime);
        }

        protected virtual void Dispose(bool disposing)
        {
            var matrices = Startup.ServiceProvider.GetRequiredService<Matrices>();
            var gameCamera = Startup.ServiceProvider.GetRequiredService<Camera>();
            var gameCommandList = Startup.ServiceProvider.GetRequiredService<GameCommandList>();
            var shaderSet = Startup.ServiceProvider.GetRequiredService<GameShaderSet>();
            var gameTextureSurface = Startup.ServiceProvider.GetRequiredService<GameTextureSurface>();

            if (!disposedValue)
            {
                if (disposing)
                {
                }

                matrices.Dispose();
                gameCamera.Dispose();
                gameCommandList.pipelineList.ForEach(e => e.Dispose());
                gameCommandList.Dispose();

                gameCommandList.assetBatchList.ForEach(e => e.Dispose());
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
