using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameTextAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Text;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return true; } }
        public override IAnimation Animation { get; set; }

        public GameTextAsset(string name, RgbaFloat color, AssetBatchType assetBatchType, float depth= 1.0f, float scale= 4.0f) :
            base(name, color, depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
            this.AssetBatchType = assetBatchType;
        }

        public override void Init(int startX, int startY, int endX, int endY, string textureName)
        {
            GameTextureData = new FontTextureData(Id);
            GameTextureData.Init();

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    QuadModel quadModel = new FontTile(new Vector2(positionInQuadCount.X, positionInQuadCount.Y), Scale, Color, Id, TextureId);
                    quadModel.Init();
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
