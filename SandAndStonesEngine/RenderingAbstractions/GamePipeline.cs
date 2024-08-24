using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.RenderingAbstractions;
using SandAndStonesEngine.Shaders;
using Veldrid;

namespace SandAndStonesEngine.GraphicAbstractions
{
    public class GamePipeline : PipelineBase
    {
        public override Viewport Viewport
        {
            get { return new Viewport(0, 0, Framebuffer.Width, Framebuffer.Height - 200, 0, 1); }
        }
        public GamePipeline(GameGraphicDevice graphicDevice, GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
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
