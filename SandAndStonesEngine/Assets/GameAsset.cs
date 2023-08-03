using System.Numerics;
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
        public GameAsset(float depth, float scale = 1.0f) : 
            base(depth, scale)
        {
        }

        public override void Init(int startX, int startY, int endX, int endY, string textureName)
        {
            SetParam(textureName);
            GameTextureData = new GameTextureData(Id, textureName);
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
