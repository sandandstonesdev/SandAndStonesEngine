using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Managers;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameStatusBarAssetBatch : GameAssetBatchBase
    {
        public override AssetBatchType AssetBatchType => AssetBatchType.StatusBarBatch;

        public GameStatusBarAssetBatch() : base()
        {
        }

        protected override void InitGameAssets()
        {
            QuadGridManager.Instance.StartNewBatch();
            List<GameAssetBase> assets = new List<GameAssetBase>();

            var GameAsset1 = new GameBackgroundAsset("status_tiles", RgbaFloat.CornflowerBlue, AssetBatchType, -1);
            GameAsset1.Init(0, 0, 8, 2, "status.png");
            AssetDataManager.Instance.Add(GameAsset1);

            var GameFontAsset1 = new GameTextAsset("point_info", RgbaFloat.Blue, AssetBatchType, 1);
            GameFontAsset1.Init(0, 0, 1, 2, "letters.png");
            GameFontAsset1.SetAnimation(new TextAnimation("Points: 100000"));
            AssetDataManager.Instance.Add(GameFontAsset1);
        }

        public override void Update(long delta)
        {
            AssetDataManager.Instance.Assets.ForEach(e =>
            {
                if (e.IsText && e.AssetBatchType == AssetBatchType.StatusBarBatch)
                {
                    string pointsText = "Points: 100000";
                    e.SetParam(pointsText);
                }
            });

            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
