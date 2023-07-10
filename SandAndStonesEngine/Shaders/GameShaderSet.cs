using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Assets;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
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
        public ResourceLayout MatricesLayout;
        public ResourceLayout WorldLayout;

        public DeviceBuffer ProjectionBuffer;
        public DeviceBuffer ViewBuffer;
        public DeviceBuffer WorldBuffer;

        public ResourceSet MatricesSet;
        public ResourceSet WorldSet;

        public Matrix4x4 ProjectionMatrix
        {
            get { return matrices.ProjectionMatrix; }
        }
        public Matrix4x4 ViewMatrix
        {
            get { return matrices.ViewMatrix; }
        }

        public Matrix4x4 WorldMatrix
        {
            get { return matrices.WorldMatrix; }
        }


        readonly Matrices matrices;
        public GameShaderSet(GameGraphicDevice gameGraphicDevice, GameAssets assets, Matrices matrices)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.assets = assets;
            this.matrices = matrices;
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

            MatricesLayout = matrices.MatricesLayout;
            WorldLayout = matrices.WorldLayout;

            MatricesSet = matrices.MatricesSet;
            WorldSet = matrices.WorldSet;


            ProjectionBuffer = matrices.ProjectionBuffer;
            ViewBuffer = matrices.ViewBuffer;
            WorldBuffer = matrices.WorldBuffer;
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
