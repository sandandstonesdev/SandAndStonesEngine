using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.DTOs;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Managers;
using SandAndStonesEngine.MathModule;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public class GameStatusBarAssetBatch : GameAssetBatchBase
    {
        public override List<IQuadModel> Assets => assetFactory.StatusBarModels;
        public override AssetBatchType AssetBatchType => AssetBatchType.StatusBarBatch;

        public readonly QuadGridManager quadGridManager;
        
        public GameStatusBarAssetBatch(AssetFactory assetFactory, GameGraphicDevice graphicDevice, QuadGridManager quadGridManager, ScrollableViewport scrollableViewport) : base(assetFactory, graphicDevice, scrollableViewport)
        {
            this.quadGridManager = quadGridManager;
        }

        protected async override void InitGameAssets()
        {
            quadGridManager.StartNewBatch();

            assetFactory.Add(assetFactory.CreateGameAsset("status_tiles", new AssetPosInfo(new Vector2(0, 0), new Vector2(8, 2)), assetFactory, scrollableViewport, quadGridManager, null!, AssetBatchType, AssetType.Background, RgbaFloat.CornflowerBlue, string.Empty, ["status.png"], 1.0f, 1.0f));
            assetFactory.Add(assetFactory.CreateGameAsset("point_info", new AssetPosInfo(new Vector2(0, 0), new Vector2(1, 1)), assetFactory, scrollableViewport, quadGridManager, null!, AssetBatchType, AssetType.PointsText, RgbaFloat.Blue, "Points: 0", ["letters.png"], 1.0f, 1.0f));

            base.InitGameAssets();
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
