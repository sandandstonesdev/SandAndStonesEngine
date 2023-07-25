using SandAndStonesEngine.GameFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.StartupUtilities;

namespace SandAndStonesEngine.GraphicAbstractions
{
    public class GameGraphicDevice : IDisposable
    {
        private static readonly Lazy<GameGraphicDevice> lazyInstance = new Lazy<GameGraphicDevice>(() => 
        {   
            GameGraphicDevice gameGraphicDevice = new GameGraphicDevice();
            gameGraphicDevice.Init();
            return gameGraphicDevice;
        });
        public static GameGraphicDevice Instance => lazyInstance.Value;

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

        public GameGraphicDevice()
        {
        }
        public void Init()
        {
            GraphicsDeviceOptions options = new GraphicsDeviceOptions(true);
            GameWindow gameWindow = Factory.Instance.GetGameWindow();
            GraphicsDevice = VeldridStartup.CreateGraphicsDevice(gameWindow.SDLWindow, options);
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
                GameWindow gameWindow = Factory.Instance.GetGameWindow();
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
