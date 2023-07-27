using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.RenderingAbstractions;
using Veldrid;
using Vulkan;
using Vulkan.Xlib;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GameCommandList : IDisposable
    {
        public CommandList CommandList;
        private bool disposedValue;
        private readonly GameAssets assets;
        private readonly GamePipeline gamePipeline;
        private readonly StatusBarPipeline statusBarPipeline;
        private readonly GameStatusBarAssets statusBarAssets;
        public GameCommandList(GameAssets assets, GameStatusBarAssets statusBarAssets, GamePipeline gamePipeline, StatusBarPipeline statusBarPipeline)
        {
            this.assets = assets;
            this.statusBarAssets = statusBarAssets;
            this.gamePipeline = gamePipeline;
            this.statusBarPipeline = statusBarPipeline;
        }

        public void Init()
        {
            ResourceFactory factory = Factory.Instance.GetResourceFactory();
            CommandList = factory.CreateCommandList();
        }

        public void Draw(float deltaTime)
        {
            GameGraphicDevice gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();

            CommandList.Begin();

            CommandList.UpdateBuffer(assets.Matrices.ProjectionBuffer, 0, assets.Matrices.ProjectionMatrix);
            CommandList.UpdateBuffer(assets.Matrices.ViewBuffer, 0, assets.Matrices.ViewMatrix);
            CommandList.UpdateBuffer(assets.Matrices.WorldBuffer, 0, assets.Matrices.WorldMatrix);

            CommandList.SetFramebuffer(gameGraphicDevice.SwapChain);

            Framebuffer frameBuffer = gameGraphicDevice.SwapChain;
            var viewport1 = new Viewport(0, 0, frameBuffer.Width, frameBuffer.Height - 200, 0, 1);
            CommandList.SetViewport(0, viewport1);
            CommandList.ClearColorTarget(0, RgbaFloat.Black);
            
            CommandList.SetPipeline(gamePipeline.Pipeline);
            CommandList.SetVertexBuffer(0, assets.DeviceVertexBuffer);
            CommandList.SetIndexBuffer(assets.DeviceIndexBuffer, assets.IndexBufferFormat);

            CommandList.SetGraphicsResourceSet(0, assets.ResourceSet);
            CommandList.SetGraphicsResourceSet(1, assets.Matrices.MatricesSet);
            CommandList.SetGraphicsResourceSet(2, assets.Matrices.WorldSet);
            CommandList.DrawIndexed(
                indexCount: assets.IndicesCount,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);

            var viewport2 = new Viewport(1, frameBuffer.Height - 200, frameBuffer.Width, frameBuffer.Height, 0, 1);
            CommandList.SetViewport(0, viewport2);

            CommandList.SetPipeline(statusBarPipeline.Pipeline);
            CommandList.SetVertexBuffer(0, statusBarAssets.DeviceVertexBuffer);
            CommandList.SetIndexBuffer(statusBarAssets.DeviceIndexBuffer, statusBarAssets.IndexBufferFormat);

            CommandList.SetGraphicsResourceSet(0, statusBarAssets.ResourceSet);
            CommandList.SetGraphicsResourceSet(1, statusBarAssets.Matrices.MatricesSet);
            CommandList.SetGraphicsResourceSet(2, statusBarAssets.Matrices.WorldSet);
            CommandList.DrawIndexed(
                indexCount: statusBarAssets.IndicesCount,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);

            //CommandList.SetFullViewports();
            CommandList.End();
            gameGraphicDevice.Flush(CommandList);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                CommandList.Dispose();
                disposedValue = true;
            }
        }

        ~GameCommandList()
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
