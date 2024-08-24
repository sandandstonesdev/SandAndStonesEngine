using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Shaders;
using Veldrid;

namespace SandAndStonesEngine.RenderingAbstractions
{
    public class StatusBarPipeline : PipelineBase
    {
        public override Viewport Viewport
        {
            get { return new Viewport(0, Framebuffer.Height - 200, Framebuffer.Width, Framebuffer.Height, 0, 1); }
        }
        public StatusBarPipeline(GameGraphicDevice graphicDevice, GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
            : base(graphicDevice, shaderSet, gameTextureSurface, matrices)
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
