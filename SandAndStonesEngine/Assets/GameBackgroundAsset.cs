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
        public ScrollableViewport scrollableViewport;
        bool scrollMoving = false;

        public GameBackgroundAsset(string name, ScrollableViewport scrollableViewport, RgbaFloat color, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, color, depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
            this.AssetBatchType = assetBatchType;
            this.scrollableViewport = scrollableViewport;
            this.scrollableViewport.ScrollChanged += ScrollChanged; 
        }

        void ScrollChanged(object? sender, EventArgs args)
        {
            scrollMoving = true;
        }

        public override void Init(int startX, int startY, int endX, int endY, string textureName)
        {
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
                x.Move(new Vector4(
                    scrollableViewport.CartesianCoords.Item1, scrollableViewport.CartesianCoords.Item2, 0, 0));
            });
        }

        public override void Update(long delta)
        {
            if (AssetBatchType == AssetBatchType.ClientRectBatch && scrollMoving)
            {
                Scroll(scrollableViewport);
                
                scrollMoving = false;
            }

            Animate();

            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            this.scrollableViewport.ScrollChanged -= ScrollChanged;
            base.Dispose(disposing);
        }
    }
}
