using SandAndStonesEngine.Assets.Batches;

namespace SandAndStonesEngine.Assets.Assets
{
    public class GameFPSCounterTextAsset : GameTextAsset
    {
        public override AssetType AssetType => AssetType.FPSText;
        public GameFPSCounterTextAsset(string name, AssetBatchType assetBatchType, float scale, float depth = 1.0f) :
            base(name, assetBatchType, depth, scale)
        {
            Id = IdManager.GetAssetId(AssetType);
            AssetBatchType = assetBatchType;
        }

        public override void Update(long delta)
        {
            Animate(FPSCalculator.Instance.GetFormatedResult());
            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
