using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.RenderingAbstractions;
using Veldrid;
using Vulkan;
using Vulkan.Xlib;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GameCommandList : IDisposable
    {
        private CommandList CommandList;
        private bool disposedValue;
        private readonly List<GameAssetBatchBase> assetBatchList;
        private readonly List<PipelineBase> pipelineList;
        private readonly Matrices matrices;
        private readonly GameTextureSurface gameTextureSurface;
        public RgbaFloat ClearColor;
        public GameCommandList(Matrices matrices, GameTextureSurface gameTextureSurface, List<GameAssetBatchBase> assetBatchList, List<PipelineBase> pipelineList)
        {
            this.ClearColor = RgbaFloat.Black;
            this.matrices = matrices;
            this.gameTextureSurface = gameTextureSurface;
            this.assetBatchList = assetBatchList;
            this.pipelineList = pipelineList;
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

            CommandList.UpdateBuffer(matrices.ProjectionBuffer, 0, matrices.ProjectionMatrix);
            CommandList.UpdateBuffer(matrices.ViewBuffer, 0, matrices.ViewMatrix);
            CommandList.UpdateBuffer(matrices.WorldBuffer, 0, matrices.WorldMatrix);

            CommandList.SetFramebuffer(gameGraphicDevice.SwapChain);
            CommandList.ClearColorTarget(0, ClearColor);

            DrawPipeline(pipelineList[0], assetBatchList[0]);
            DrawPipeline(pipelineList[1], assetBatchList[1]);

            CommandList.End();
            gameGraphicDevice.Flush(CommandList);
        }

        private void DrawPipeline(PipelineBase pipeline, GameAssetBatchBase assetBatch)
        {
            CommandList.SetViewport(0, pipeline.Viewport);

            CommandList.SetPipeline(pipeline.Pipeline);
            CommandList.SetVertexBuffer(0, assetBatch.DeviceVertexBuffer);
            CommandList.SetIndexBuffer(assetBatch.DeviceIndexBuffer, assetBatch.IndexBufferFormat);

            CommandList.SetGraphicsResourceSet(0, gameTextureSurface.ResourceSet);
            CommandList.SetGraphicsResourceSet(1, matrices.MatricesSet);
            CommandList.SetGraphicsResourceSet(2, matrices.WorldSet);
            CommandList.DrawIndexed(
                indexCount: assetBatch.IndicesCount,
                instanceCount: 1,
                indexStart: 0,
                vertexOffset: 0,
                instanceStart: 0);
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
