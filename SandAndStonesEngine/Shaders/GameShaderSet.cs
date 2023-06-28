using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.GraphicAbstractions;
using Veldrid;
using Veldrid.SPIRV;

namespace SandAndStonesEngine.Shaders
{
    internal class GameShaderSet
    {
        public Shader[] Shaders { get; private set; }
        private readonly GameGraphicDevice gameGraphicDevice;
        private readonly GameAssets assets;
        public ShaderSetDescription ShaderSet;
        readonly Dictionary<string, string> shaderFileNames = new Dictionary<string, string>
        {
            { "VS", "vertexShader.vs"},
            { "PS", "pixelShader.ps" }
        };

        public ResourceSet ResourceSet;
        public ResourceLayout ResourceLayout;
        public GameShaderSet(GameGraphicDevice gameGraphicDevice, GameAssets assets)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.assets = assets;
        }

        public void Create()
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

            ResourceSet = assets.ResourceSet;
            ResourceLayout = assets.gameTexture.TextureLayout;

        }

        public void Destroy()
        {
            foreach (var shader in Shaders)
            {
                shader.Dispose();
            }
        }
    }
}
