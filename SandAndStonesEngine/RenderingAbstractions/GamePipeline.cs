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
    public class GamePipeline : PipelineBase
    {
        public override Viewport Viewport
        {
            get { return new Viewport(0, 0, Framebuffer.Width, Framebuffer.Height - 200, 0, 1); }
        }
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
