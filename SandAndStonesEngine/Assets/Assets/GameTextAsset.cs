using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using System.Numerics;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameTextAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Text;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return true; } }
        public override IAnimation Animation { get; set; }

        public GameTextAsset(string name, AssetBatchType assetBatchType, float depth = 1.0f, float scale = 1.0f) :
            base(name, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
        }

        public override GameAssetBase Init(QuadGridManager quadGridManager, AssetInfo assetInfo)
        {
            Animation = assetInfo.Animation;
            GameTextureData = assetInfo.AssetFactory.CreateTexture(Id, assetInfo.Textures[0].Name, TextureType.Text);

            for (int i = (int)assetInfo.StartPos.X; i < assetInfo.EndPos.X; i++)
            {
                for (int j = (int)assetInfo.StartPos.Y; j < assetInfo.EndPos.Y; j++)
                {
                    CreateAssetQuad(quadGridManager, assetInfo, new Vector2(0, 0), new Vector3(i, j, Depth), TileType.Font);
                }
            }

            return this;
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
