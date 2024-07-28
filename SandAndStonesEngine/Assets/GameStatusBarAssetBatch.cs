using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Managers;
using Veldrid;

namespace SandAndStonesEngine.Assets
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
            
            var GameAsset1 = new GameBackgroundAsset("status_tiles", scrollableViewport, RgbaFloat.CornflowerBlue, AssetBatchType, -1);
            GameAsset1.Init(0, 0, 8, 2, "status.png");
            GameAsset1.SetAnimation(new TextureAnimation("status.png"));
            AssetDataManager.Instance.Add(GameAsset1);

            var GameFontAsset1 = new GamePointsCounterTextAsset("point_info", RgbaFloat.Blue, AssetBatchType, 1.0f, 1.0f);
            GameFontAsset1.Init(0, 0, 1, 1, "letters.png");
            GameFontAsset1.SetAnimation(new TextAnimation("Points: 100000"));
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
