using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using Veldrid;
using Veldrid.SPIRV;

namespace SandAndStonesEngine.Shaders
{
    public class GameShaderSet : IDisposable
    {
        public Shader[] Shaders { get; private set; }
        private readonly GameAssetBatchBase assets;
        public ShaderSetDescription ShaderSet;
        private bool disposedValue;
        readonly Dictionary<string, string> shaderFileNames = new Dictionary<string, string>
        {
            { "VS", "vertexShader.vs"},
            { "PS", "pixelShader.ps" }
        };

        protected readonly Matrices matrices;
        protected readonly GameGraphicDevice gameGraphicDevice;

        public GameShaderSet(GameGraphicDevice gameGraphicDevice, GameAssetBatchBase assets, Matrices matrices)
        {
            this.assets = assets;
            this.matrices = matrices;
            this.gameGraphicDevice = gameGraphicDevice;
        }

        public void Init()
        {
            ResourceFactory factory = gameGraphicDevice.ResourceFactory;

            ShaderProgram vertexShader = new ShaderProgram(shaderFileNames["VS"], ShaderStages.Vertex);
            vertexShader.Init();
            ShaderProgram pixelShader = new ShaderProgram(shaderFileNames["PS"], ShaderStages.Fragment);
            pixelShader.Init();

            Shaders = factory.CreateFromSpirv(vertexShader.ShaderDesc, pixelShader.ShaderDesc);
            ShaderSet = new ShaderSetDescription(
                vertexLayouts: assets.VertexLayouts,
                shaders: Shaders);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                foreach (var shader in Shaders)
                {
                    shader?.Dispose();
                }
                disposedValue = true;
            }
        }

        ~GameShaderSet()
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
