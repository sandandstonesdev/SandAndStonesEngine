using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameFPSCounterTextAsset : GameTextAsset
    {
        public GameFPSCounterTextAsset(string name, RgbaFloat color, AssetBatchType assetBatchType, float depth = 1.0f, float scale = 4.0f) :
            base(name, color, assetBatchType, depth, scale)
        {
            this.Id = IdManager.GetAssetId(AssetType);
            this.AssetBatchType = assetBatchType;
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
