using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameBackgroundAsset : GameAssetBase
    {
        protected override AssetType AssetType => AssetType.Background;
        public override bool IsText { get { return false; } }
        public GameBackgroundAsset(RgbaFloat color, float depth, float scale = 1.0f) :
            base(color,depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
        }

        public override void Init(int startX, int startY, int endX, int endY, string textureName)
        {
            SetParam(textureName);
            GameTextureData = new GameTextureData(Id, textureName);
            GameTextureData.Init();

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    QuadModel quadModel = new BackgroundTile(new Vector2(positionInQuadCount.X, positionInQuadCount.Y), positionInQuadCount.Z, Scale, Color, Id, TextureId);
                    quadModel.Init();
                    QuadModelList.Add(quadModel);
                }
            }
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
