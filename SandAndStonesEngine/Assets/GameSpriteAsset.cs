using System.Numerics;
using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameSpriteAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Sprite;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return false; } }
        public override IAnimation Animation { get; set; }

        public GameSpriteAsset(string name, RgbaFloat color, AssetBatchType assetBatchType, float depth, float scale = 1.0f) : 
            base(name, color, depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
            this.AssetBatchType = assetBatchType;
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
                    QuadModel quadModel = new SpriteTile(new Vector2(positionInQuadCount.X, positionInQuadCount.Y), Scale, Color, Id, TextureId);
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
