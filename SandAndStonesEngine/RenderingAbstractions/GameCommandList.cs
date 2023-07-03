using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.Buffers;
using Veldrid;
using Vulkan.Xlib;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GameCommandList
    {
        public CommandList CommandList;
        private readonly GameGraphicDevice gameGraphicDevice;

        private readonly GameAssets assets;
        private readonly GamePipeline gamePipeline;
        public GameCommandList(GameGraphicDevice gameGraphicDevice, GameAssets assets, GamePipeline gamePipeline)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.assets = assets;
            this.gamePipeline = gamePipeline;
        }

        public void Create()
        {
            ResourceFactory factory = gameGraphicDevice.ResourceFactory;
            CommandList = factory.CreateCommandList();
        }

        public void Draw(float deltaTime)
        {
            CommandList.Begin();

            CommandList.UpdateBuffer(gamePipeline.Camera.ProjectionBuffer, 0, gamePipeline.Camera.ProjectionMatrix);
            CommandList.UpdateBuffer(gamePipeline.Camera.ViewBuffer, 0, gamePipeline.Camera.ViewMatrix);
            CommandList.UpdateBuffer(gamePipeline.Camera.WorldBuffer, 0, gamePipeline.Camera.WorldMatrix);

            //string debugGroup = "Debug1";
            //CommandList.PushDebugGroup(debugGroup);
            //CommandList.PopDebugGroup();

            CommandList.SetFramebuffer(gameGraphicDevice.SwapChain);
            CommandList.ClearColorTarget(0, assets.ClearColor);
            //CommandList.ClearDepthStencil(1f);
            CommandList.SetPipeline(gamePipeline.Pipeline);
            CommandList.SetVertexBuffer(0, assets.DeviceVertexBuffer);
            CommandList.SetIndexBuffer(assets.DeviceIndexBuffer, assets.IndexBufferFormat);
            CommandList.SetGraphicsResourceSet(0, assets.ResourceSet);
            CommandList.SetGraphicsResourceSet(1, gamePipeline.Camera.MatricesSet);
            CommandList.SetGraphicsResourceSet(2, gamePipeline.Camera.WorldSet);
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
