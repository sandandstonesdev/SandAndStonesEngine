using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Shaders;
using Veldrid;
using Vulkan;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GamePipeline : IDisposable
    {
        public GameAssets gameAssets;
        public Pipeline Pipeline;
        public Matrices matrices;
        GameShaderSet shaderSet;
        private bool disposedValue;

        public GamePipeline(GameShaderSet shaderSet, GameAssets gameAssets, Matrices matrices)
        {
            this.gameAssets = gameAssets;
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
                ResourceLayouts = new ResourceLayout[] { gameAssets.ResourceLayout, matrices.MatricesLayout, matrices.WorldLayout }, 
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
