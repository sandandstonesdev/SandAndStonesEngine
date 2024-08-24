using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesEngine.GameFactories;
using System.Numerics;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameSpriteAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Sprite;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return false; } }
        public override IAnimation Animation { get; set; }

        public GameSpriteAsset(string name, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
        }

        public override void Init(QuadGridManager quadGridManager, AssetInfo assetInfo)
        {
            Animation = assetInfo.Animation;
            GameTextureData = AssetFactory.Instance.CreateTexture(Id, assetInfo.Textures[0].Name, TextureType.Standard);
            GameTextureData.Init();

            for (int i = (int)assetInfo.StartPos.X; i < assetInfo.EndPos.X; i++)
            {
                for (int j = (int)assetInfo.StartPos.Y; j < assetInfo.EndPos.Y; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    //var screenPosition = new Vector2((int)i / 8, 0);
                    var quadData = quadGridManager.GetQuadData(new Vector2(0, 0), positionInQuadCount, TileType.Sprite);
                    var quadModel = AssetFactory.Instance.CreateTile(quadData, Scale, assetInfo.Textures[0].Color, Id, TextureId, TileType.Sprite);
                    quadModel.Init(quadGridManager.screenDivision);
                    QuadModelList.Add(quadModel);
                }
            }
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
