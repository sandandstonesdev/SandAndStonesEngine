using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        FPSCalculator fpsCalculator;
        public GameAssetBatch() : base()
        {
            this.fpsCalculator = new FPSCalculator(10);
        }

        protected override List<GameAssetBase> InitGameAssets()
        {
            int assetId = 0;
            List<GameAssetBase> assets = new List<GameAssetBase>();

            var BackgroundAsset = new GameAsset(-1);
            BackgroundAsset.Init(0, 0, 4, "wall.png");
            assets.Add(BackgroundAsset);

            var GameAsset1 = new GameAsset(1, 0.5f);
            GameAsset1.Init(0, 0, 1, "char1.png");
            assets.Add(GameAsset1);

            var GameAsset2 = new GameAsset(0.5f);
            GameAsset2.Init(1, 1, 2, "char2.png");
            assets.Add(GameAsset2);

            var GameFontAsset1 = new GameTextAsset(1);
            GameFontAsset1.Init(0, 0, 1, "letters.png");
            assets.Add(GameFontAsset1);
            return assets;
        }
        
        public override void Update(long delta)
        {
            fpsCalculator.AddNextDelta(delta);

            if (fpsCalculator.CanDoUpdate(delta))
            {
                int fps = (int)fpsCalculator.GetResult();
                string text = $"FPS: {fps}";
                
                Assets.ForEach(e =>
                {
                    if (e.IsText)
                    {
                        e.SetParam(text);
                    }
                });
            }

            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
