using Microsoft.Extensions.DependencyInjection;
using SandAndStonesEngine.GameFactories;
using Veldrid;
using Veldrid.StartupUtilities;

namespace SandAndStonesEngine.GraphicAbstractions
{
    public class GameGraphicDevice : IDisposable
    {
        public GraphicsDevice GraphicsDevice;

        public Framebuffer SwapChain
        {
            get { return GraphicsDevice.SwapchainFramebuffer; }
        }
        public ResourceFactory ResourceFactory
        {
            get { return GraphicsDevice.ResourceFactory; }
        }

        public bool Initialized = false;
        private bool disposedValue;
        private readonly GameWindow window;

        public GameGraphicDevice(GameWindow window)
        {
            this.window = window;
        }

        public void Init()
        {
            GraphicsDevice = VeldridStartup.CreateGraphicsDevice(window.SDLWindow, new GraphicsDeviceOptions(true));
            Initialized = true;
        }

        public void Flush(CommandList commandList)
        {
            GraphicsDevice.SwapBuffers();
            GraphicsDevice.SubmitCommands(commandList);
            GraphicsDevice.WaitForIdle();
        }

        public void ResizeWindow(uint width, uint height)
        {
            GraphicsDevice.ResizeMainWindow(width, height);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                GraphicsDevice.Dispose();
                GameWindow gameWindow = Startup.ServiceProvider.GetRequiredService<GameWindow>();
                gameWindow.Dispose();
                disposedValue = true;
            }
        }

        ~GameGraphicDevice()
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
