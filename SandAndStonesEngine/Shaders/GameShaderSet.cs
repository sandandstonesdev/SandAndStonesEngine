using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using Veldrid;
using Veldrid.SPIRV;

namespace SandAndStonesEngine.Shaders
{
    internal class GameShaderSet
    {
        public Shader[] Shaders { get; private set; }
        private readonly GameAssets assets;
        public ShaderSetDescription ShaderSet;
        readonly Dictionary<string, string> shaderFileNames = new Dictionary<string, string>
        {
            { "VS", "vertexShader.vs"},
            { "PS", "pixelShader.ps" }
        };

        readonly Matrices matrices;
        public GameShaderSet(GameAssets assets, Matrices matrices)
        {
            this.assets = assets;
            this.matrices = matrices;
        }

        public void Create()
        {
            ResourceFactory factory = Factory.Instance.GetResourceFactory();
            
            
            ShaderProgram vertexShader = new ShaderProgram(shaderFileNames["VS"], ShaderStages.Vertex);
            vertexShader.Init();
            ShaderProgram pixelShader = new ShaderProgram(shaderFileNames["PS"], ShaderStages.Fragment);
            pixelShader.Init();

            Shaders = factory.CreateFromSpirv(vertexShader.ShaderDesc, pixelShader.ShaderDesc);
            ShaderSet = new ShaderSetDescription(
                vertexLayouts: assets.VertexLayouts,
                shaders: Shaders);
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
