using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.GameFactories;
using Veldrid;
using Vulkan.Xlib;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GameCommandList
    {
        public CommandList CommandList;

        private readonly GameAssets assets;
        private readonly GamePipeline gamePipeline;
        public GameCommandList(GameAssets assets, GamePipeline gamePipeline)
        {
            this.assets = assets;
            this.gamePipeline = gamePipeline;
        }

        public void Create()
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
            CommandList.ClearColorTarget(0, assets.ClearColor);
            //CommandList.ClearDepthStencil(1f);
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
            CommandList.End();
            gameGraphicDevice.Flush(CommandList);
        }

        public void Destroy()
        {
            CommandList.Dispose();
        }
    }
}
