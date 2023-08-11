using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using static System.Net.Mime.MediaTypeNames;

namespace SandAndStonesEngine.Assets
{
    public class GameStatusBarAssetBatch : GameAssetBatchBase
    {
        public GameStatusBarAssetBatch() : base()
        {
        }

        protected override List<GameAssetBase> InitGameAssets()
        {
            QuadGridManager.Instance.StartNewBatch();
            List<GameAssetBase> assets = new List<GameAssetBase>();

            var GameAsset1 = new GameBackgroundAsset("status_tiles", RgbaFloat.CornflowerBlue, -1);
            GameAsset1.Init(0, 0, 8, 2, "status.png");
            assets.Add(GameAsset1);

            var GameFontAsset1 = new GameTextAsset("point_info", RgbaFloat.Blue, 1);
            GameFontAsset1.Init(0, 0, 1, 2, "letters.png");
            assets.Add(GameFontAsset1);
            return assets;
        }

        public override void Update(long delta)
        {
            Assets.ForEach(e =>
            {
                if (e.IsText)
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
