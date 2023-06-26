using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.Buffers;
using Veldrid;

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

        public void Draw()
        {
            CommandList.Begin();
            CommandList.SetFramebuffer(gameGraphicDevice.SwapChain);
            CommandList.ClearColorTarget(0, assets.ClearColor);
            CommandList.SetVertexBuffer(0, assets.DeviceVertexBuffer);
            CommandList.SetIndexBuffer(assets.DeviceIndexBuffer, assets.IndexBufferFormat);
            CommandList.SetPipeline(gamePipeline.Pipeline);
            CommandList.DrawIndexed(
                indexCount: assets.IndicesCount,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);
            CommandList.End();
            gameGraphicDevice.Flush();
            gameGraphicDevice.GraphicsDevice.SubmitCommands(CommandList);
        }

        public void Destroy()
        {
            CommandList.Dispose();
        }
    }
}
