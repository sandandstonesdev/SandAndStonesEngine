﻿using System.Numerics;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Utils;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAsset : GameAssetBase
    {
        public override bool IsText { get { return false; } }
        public GameAsset(int textureId, float depth, float scale = 1.0f) : 
            base(textureId, depth, scale)
        {
        }

        public void Init(int startX, int startY, int end, QuadGrid quadGrid, string textureName)
        {
            SetParam(textureName);
            GameTextureData = new GameTextureData(Id, TextureId, textureName);
            GameTextureData.Init();

            base.Init(startX, startY, end, quadGrid, textureName);
        }


        public override void SetParam(object param)
        {
            base.SetParam(param);
        }

        public override void Update(double delta)
        {
            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose();
        }
    }
}
