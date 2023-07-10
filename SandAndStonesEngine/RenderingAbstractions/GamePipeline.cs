using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.Shaders;
using Veldrid;
using Vulkan;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GamePipeline
    {
        private readonly GameGraphicDevice gameGraphicDevice;
        public GameShaderSet ShaderBatch;
        public Pipeline Pipeline;
        public Camera Camera;

        public GamePipeline(GameGraphicDevice gameGraphicDevice, GameShaderSet shaderBatch, Camera camera)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.ShaderBatch = shaderBatch;
            this.Camera = camera;
        }

        public void Create()
        {
            ResourceFactory factory = gameGraphicDevice.ResourceFactory;

            GraphicsPipelineDescription pipelineDescription = new()
            {
                BlendState = BlendStateDescription.SingleOverrideBlend,

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
                ResourceLayouts = new ResourceLayout[] { ShaderBatch.ResourceLayout, ShaderBatch.MatricesLayout, ShaderBatch.WorldLayout }, 
                ShaderSet = ShaderBatch.ShaderSet,

                Outputs = gameGraphicDevice.SwapChain.OutputDescription
            };
            Pipeline = factory.CreateGraphicsPipeline(pipelineDescription);
        }

        public void Destroy()
        {
            Pipeline.Dispose();
        }
    }
}
