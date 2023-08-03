using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Vortice.DXGI;

namespace SandAndStonesEngine.RenderingAbstractions
{
    public abstract class PipelineBase : IDisposable
    {
        private Matrices matrices;
        private GameShaderSet shaderSet;
        private GameTextureSurface gameTextureSurface;
        public Pipeline Pipeline;
        protected GameGraphicDevice gameGraphicDevice;
        private bool disposedValue;
        protected Framebuffer Framebuffer
        {
            get { return gameGraphicDevice.SwapChain; }
        }
        public abstract Viewport Viewport { get; }

        public PipelineBase(GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
        {
            this.gameTextureSurface = gameTextureSurface;
            this.matrices = matrices;
            this.shaderSet = shaderSet;
            this.gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
        }

        public virtual void Init()
        {
            ResourceFactory factory = Factory.Instance.GetResourceFactory();

            GraphicsPipelineDescription pipelineDescription = new()
            {
                BlendState = BlendStateDescription.SingleAlphaBlend,

                DepthStencilState = new DepthStencilStateDescription(
                depthTestEnabled: true,
                depthWriteEnabled: true,
                comparisonKind: ComparisonKind.LessEqual),

                RasterizerState = new RasterizerStateDescription(
                cullMode: FaceCullMode.Back,
                fillMode: PolygonFillMode.Solid,
                frontFace: FrontFace.Clockwise,
                depthClipEnabled: true,
                scissorTestEnabled: false),

                PrimitiveTopology = PrimitiveTopology.TriangleList,
                ResourceLayouts = new ResourceLayout[] { gameTextureSurface.TextureLayout, matrices.MatricesLayout, matrices.WorldLayout },
                ShaderSet = shaderSet.ShaderSet,

                Outputs = gameGraphicDevice.SwapChain.OutputDescription
            };
            Pipeline = factory.CreateGraphicsPipeline(pipelineDescription);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                Pipeline.Dispose();
                disposedValue = true;
            }
        }

        ~PipelineBase()
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
