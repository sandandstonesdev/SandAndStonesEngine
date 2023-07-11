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
    public class GameGraphicDevice
    {
        private static readonly Lazy<GameGraphicDevice> lazyInstance = new Lazy<GameGraphicDevice>(() => 
        {   
            GameGraphicDevice gameGraphicDevice = new GameGraphicDevice();
            gameGraphicDevice.Create();
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
        public GameGraphicDevice()
        {
        }
        public void Create()
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

        public void Destroy()
        {
            GraphicsDevice.Dispose();
        }
    }
}
