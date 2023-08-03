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
using SandAndStonesEngine.RenderingAbstractions;
using SandAndStonesEngine.Shaders;
using Veldrid;
using Vulkan;

namespace SandAndStonesEngine.GraphicAbstractions
{
    internal class GamePipeline : PipelineBase
    {
        public GamePipeline(GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
            : base(shaderSet, gameTextureSurface, matrices)
        { 
        }

        public override void Init()
        {
            base.Init();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
