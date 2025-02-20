using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesLibrary.AssetConfig;
using SandAndStonesLibrary.Assets;
using System.Numerics;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameBackgroundAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Background;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return false; } }
        public ScrollableViewport scrollableViewport;

        private bool scrollMoving = false;

        public GameBackgroundAsset(string name, ScrollableViewport scrollableViewport, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
            this.scrollableViewport = scrollableViewport;
            this.scrollableViewport.ScrollChanged += ScrollChanged;
        }

        void ScrollChanged(object? sender, EventArgs args)
        {
            scrollMoving = true;
        }

        public override GameAssetBase Init(QuadGridManager quadGridManager, AssetInfo assetInfo)
        {
            Animation = assetInfo.Animation;
            TextureInfo = assetInfo.Textures[0];

            for (int i = (int)assetInfo.StartPos.X; i < assetInfo.EndPos.X; i++)
            {
                for (int j = (int)assetInfo.StartPos.Y; j < assetInfo.EndPos.Y; j++)
                {
                    CreateAssetQuad(quadGridManager, assetInfo, new Vector2(i / 8, j / 8), new Vector3(i % 8, j % 8, Depth), TileType.Background);
                }
            }

            return this;
        }

        public void Scroll(ScrollableViewport scrollableViewport)
        {
            QuadModelList.ForEach(x =>
            {
                x.Move(new Vector4(
                    scrollableViewport.CartesianCoords.Item1,
                    scrollableViewport.CartesianCoords.Item2,
                    0,
                    0));
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
            scrollableViewport.ScrollChanged -= ScrollChanged;
            base.Dispose(disposing);
        }
    }
}
