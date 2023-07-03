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
        private readonly GameWindow window;
        public GraphicsDevice GraphicsDevice;

        public GameWindow GameWindow
        {
            get { return window; }
        }
        public Framebuffer SwapChain
        {
            get { return GraphicsDevice.SwapchainFramebuffer; }
        }
        public ResourceFactory ResourceFactory
        {
            get { return GraphicsDevice.ResourceFactory; }
        }
        public GameGraphicDevice(GameWindow window)
        {
            this.window = window;
        }
        public void Create()
        {
            GraphicsDeviceOptions options = new GraphicsDeviceOptions(true);
            GraphicsDevice = VeldridStartup.CreateGraphicsDevice(window.SDLWindow, options);
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
