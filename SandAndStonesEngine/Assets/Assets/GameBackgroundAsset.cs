using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.MathModule;
using System.Numerics;
using TextureType = SandAndStonesEngine.Assets.Textures.TextureType;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameBackgroundAsset : GameAssetBase
    {
        public override AssetType AssetType => AssetType.Background;
        public override AssetBatchType AssetBatchType { get; init; }
        public override bool IsText { get { return false; } }
        public override IAnimation Animation { get; set; }
        public ScrollableViewport scrollableViewport;

        private bool scrollMoving = false;
        private readonly ViewTransformator viewTransformator;

        public GameBackgroundAsset(string name, ViewTransformator viewTransformator, ScrollableViewport scrollableViewport, AssetBatchType assetBatchType, float depth, float scale = 1.0f) :
            base(name, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
            this.scrollableViewport = scrollableViewport;
            this.scrollableViewport.ScrollChanged += ScrollChanged;
            this.viewTransformator = viewTransformator;
        }

        void ScrollChanged(object? sender, EventArgs args)
        {
            scrollMoving = true;
        }

        public override void Init(QuadGridManager quadGridManager, AssetInfo assetInfo)
        {
            var quadCount = assetInfo.QuadGridCount;
            Animation = assetInfo.Animation;
            GameTextureData = AssetFactory.Instance.CreateTexture(Id, assetInfo.Textures[0].Name, TextureType.Standard);
            GameTextureData.Init();

            for (int i = (int)assetInfo.StartPos.X; i < assetInfo.EndPos.X; i++)
            {
                for (int j = (int)assetInfo.StartPos.Y; j < assetInfo.EndPos.Y; j++)
                {
                    var gridQuadPosition = new Vector3(i % 8, j % 8, Depth);
                    var screenPosition = new Vector2(i / 8, j / 8);
                    var quadData = quadGridManager.GetQuadData(screenPosition, gridQuadPosition, TileType.Background);
                    var quadModel = AssetFactory.Instance.CreateTile(quadData, Scale, assetInfo.Textures[0].Color, Id, TextureId, TileType.Background);
                    quadModel.Init(quadGridManager.screenDivision);
                    QuadModelList.Add(quadModel);
                }
            }
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
