using SandAndStonesEngine.Assets;
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

namespace SandAndStonesEngine.RenderingAbstractions
{
    public class StatusBarPipeline : IDisposable
    {
        private Matrices matrices;
        private GameShaderSet shaderSet;
        private GameTextureSurface gameTextureSurface;
        public Pipeline Pipeline;
        private bool disposedValue;

        public StatusBarPipeline(GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
        {
            this.gameTextureSurface = gameTextureSurface;
            this.matrices = matrices;
            this.shaderSet = shaderSet;
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

        ~StatusBarPipeline()
        {
            Dispose(disposing: false);
        }

        public void Create()
        {
            GameGraphicDevice gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
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

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
