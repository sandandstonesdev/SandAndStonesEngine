using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.StartupUtilities;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GameGraphicDevice
    {
        private readonly GameWindow window;
        public GraphicsDevice GraphicsDevice;

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
            GraphicsDevice = VeldridStartup.CreateGraphicsDevice(window.SDLWindow);
        }

        public void Flush()
        {
            GraphicsDevice.SwapBuffers();
        }

        public void Destroy()
        {
            GraphicsDevice.Dispose();
        }
    }
}
