using SandAndStonesLibrary.AssetConfig;
using SandAndStonesLibrary.Assets;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GamePointsCounterTextAsset : GameTextAsset
    {
        private long Points = 0;
        public override AssetType AssetType => AssetType.PointsText;
        public GamePointsCounterTextAsset(string name, AssetBatchType assetBatchType, float scale, float depth = 1.0f) :
            base(name, assetBatchType, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
        }

        public override void Update(long delta)
        {
            if (AssetBatchType == AssetBatchType.StatusBarBatch)
            {
                Points += delta;
                Animate($"Points: {Points}", 2);
            }

            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
