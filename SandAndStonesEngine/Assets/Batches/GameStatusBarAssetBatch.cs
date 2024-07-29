using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.Managers;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public class GameStatusBarAssetBatch : GameAssetBatchBase
    {
        public override List<IQuadModel> Assets => AssetDataManager.Instance.StatusBarModels;
        public override AssetBatchType AssetBatchType => AssetBatchType.StatusBarBatch;

        public GameStatusBarAssetBatch(ScrollableViewport scrollableViewport) : base(scrollableViewport)
        {
        }

        protected override void InitGameAssets()
        {
            QuadGridManager.Instance.StartNewBatch();

            var GameAsset1 = new GameStatusBarBackgroundAsset("status_tiles", AssetBatchType, 1.0f, 1.0f);
            GameAsset1.Init(AssetInfo.Create2DAssetInfo(0, 0, 8, 2, new TextureAnimation("status.png"), TextureInfo.CreateTextureInfo("status.png", RgbaFloat.CornflowerBlue)));
            AssetDataManager.Instance.Add(GameAsset1);

            var GameFontAsset1 = new GamePointsCounterTextAsset("point_info", AssetBatchType, 1.0f, 1.0f);
            GameFontAsset1.Init(AssetInfo.Create2DAssetInfo(0, 0, 1, 1, new TextAnimation("Points: 0"), TextureInfo.CreateTextureInfo("letters.png", RgbaFloat.Blue)));
            AssetDataManager.Instance.Add(GameFontAsset1);

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
