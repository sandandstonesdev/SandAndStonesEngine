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
        public GameTextAsset(float depth= 1.0f, float scale= 4.0f) :
            base(depth, scale)
        {
        }

        public override void Init(int startX, int startY, int endX, int endY, string textureName)
        {
            SetParam("");
            GameTextureData = new FontTextureData(Id);
            GameTextureData.Init();

            base.Init(startX, startY, endX, endY, textureName);
        }

        public override void SetParam(object param)
        {
            base.SetParam(param);
        }

        public override void Update(long delta)
        {
            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
