using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameBackgroundAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Background;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return false; } }
        public override IAnimation Animation { get; set; }

        public GameBackgroundAsset(string name, RgbaFloat color, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
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
                    QuadModel quadModel = new BackgroundTile(new Vector2(positionInQuadCount.X, positionInQuadCount.Y), positionInQuadCount.Z, Scale, Color, Id, TextureId);
                    quadModel.Init();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public void Scroll(ScrollableViewport scrollableViewport)
        {
            QuadModelList.ForEach(x =>
            {
                var backgroundTile = x as BackgroundTile;
                backgroundTile?.Scroll(scrollableViewport);
            });
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
