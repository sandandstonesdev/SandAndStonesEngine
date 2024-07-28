using SandAndStonesEngine.MathModule;
using System.Diagnostics;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GamePointsCounterTextAsset : GameTextAsset
    {
        private long Points = 0;
        public GamePointsCounterTextAsset(string name, RgbaFloat color, AssetBatchType assetBatchType, float scale, float depth = 1.0f) :
            base(name, color, assetBatchType, depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
            this.AssetBatchType = assetBatchType;
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
