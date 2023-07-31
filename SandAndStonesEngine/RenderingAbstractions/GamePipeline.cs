using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Shaders;
using Veldrid;
using Vulkan;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GamePipeline : IDisposable
    {
        private GameAssets gameAssets;
        private Matrices matrices;
        private GameShaderSet shaderSet;
        private GameTextureSurface gameTextureSurface;
        private bool disposedValue;
        public Pipeline Pipeline;
        public GamePipeline(GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
        {
            this.gameTextureSurface = gameTextureSurface;
            this.matrices = matrices;
            this.shaderSet = shaderSet;    
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

        ~GamePipeline()
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
