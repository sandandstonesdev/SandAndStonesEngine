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
            List<GameAssetBase> assets = new List<GameAssetBase>();

            var BackgroundAsset = new GameBackgroundAsset(RgbaFloat.White, -1);
            BackgroundAsset.Init(0, 0, 8, 8, "wall.png");
            assets.Add(BackgroundAsset);

            // There we need different QuadGrid to place model with smaller scale
            // in the same rectangle region because coordinates are scaled too
            //var GameAsset1 = new GameSpriteAsset(RgbaFloat.Green, 1, 0.7f);
            //GameAsset1.Init(0, 7, 1, 8, "char1.png");
            //assets.Add(GameAsset1);

            var GameAsset2 = new GameSpriteAsset(RgbaFloat.Green, 0.5f);
            GameAsset2.Init(0, 7, 1, 8, "char2.png");
            assets.Add(GameAsset2);

            var GameAsset3 = new GameBackgroundAsset(RgbaFloat.White, -1);
            GameAsset3.Init(1, 6, 2, 7, "torch.png");
            assets.Add(GameAsset3);

            var GameAsset4 = new GameBackgroundAsset(RgbaFloat.White, -1);
            GameAsset4.Init(3, 6, 4, 7, "torch.png");
            assets.Add(GameAsset4);

            var GameAsset5 = new GameBackgroundAsset(RgbaFloat.White, -1);
            GameAsset5.Init(5, 6, 6, 7, "torch.png");
            assets.Add(GameAsset5);

            var GameAsset6 = new GameBackgroundAsset(RgbaFloat.White, -1);
            GameAsset6.Init(7, 6, 8, 7, "torch.png");
            assets.Add(GameAsset6);

            var GameFontAsset1 = new GameTextAsset(RgbaFloat.Blue ,1);
            GameFontAsset1.Init(0, 0, 1, 1, "letters.png");
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
