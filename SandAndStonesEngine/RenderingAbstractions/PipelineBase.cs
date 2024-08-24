using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Shaders;
using Veldrid;

namespace SandAndStonesEngine.RenderingAbstractions
{
    public abstract class PipelineBase : IDisposable
    {
        private Matrices matrices;
        private GameShaderSet shaderSet;
        private GameTextureSurface gameTextureSurface;
        public Pipeline Pipeline;
        protected GameGraphicDevice graphicDevice;
        private bool disposedValue;
        protected Framebuffer Framebuffer
        {
            get { return graphicDevice.SwapChain; }
        }
        public abstract Viewport Viewport { get; }

        public PipelineBase(GameGraphicDevice graphicDevice, GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
        {
            this.gameTextureSurface = gameTextureSurface;
            this.matrices = matrices;
            this.shaderSet = shaderSet;
            this.graphicDevice = graphicDevice;
        }

        public virtual void Init()
        {
            ResourceFactory factory = graphicDevice.ResourceFactory;

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

                Outputs = graphicDevice.SwapChain.OutputDescription
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
