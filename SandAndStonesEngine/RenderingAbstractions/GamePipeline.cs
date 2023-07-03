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
        private readonly GameShaderSet shaderBatch;
        public Pipeline Pipeline;
        public Camera Camera;
        public GamePipeline(GameGraphicDevice gameGraphicDevice, GameShaderSet shaderBatch, Camera camera)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.shaderBatch = shaderBatch;
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
                ResourceLayouts = new ResourceLayout[] { shaderBatch.ResourceLayout, Camera.MatricesLayout, Camera.WorldLayout  }, 
                ShaderSet = shaderBatch.ShaderSet,

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
