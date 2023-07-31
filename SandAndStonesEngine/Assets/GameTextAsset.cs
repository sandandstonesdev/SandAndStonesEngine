using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.Assets
{
    public class GameTextAsset : GameAssetBase
    {
        public override bool IsText { get { return true; } }
        public GameTextAsset(int textureId, float depth=1, float scale= 4.0f) :
            base(textureId, depth, scale)
        {
        }

        public void Init(int startX, int startY, int end, QuadGrid quadGrid, string textureName)
        {
            GameTextureData = new FontTextureData(Id, TextureId);
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
