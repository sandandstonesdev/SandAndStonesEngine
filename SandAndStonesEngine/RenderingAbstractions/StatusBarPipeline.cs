﻿using SandAndStonesEngine.Assets;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.RenderingAbstractions
{
    public class StatusBarPipeline : PipelineBase
    {
        public override Viewport Viewport
        {
            get { return new Viewport(0, Framebuffer.Height - 200, Framebuffer.Width, Framebuffer.Height, 0, 1); }
        }
        public StatusBarPipeline(GameShaderSet shaderSet, GameTextureSurface gameTextureSurface, Matrices matrices)
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
