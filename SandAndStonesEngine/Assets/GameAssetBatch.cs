using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.MathModule;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        FPSCalculator fpsCalculator;
        ViewTransformator viewTransformator;
        bool characterMoving = false;
        bool scrollMoving = false;
        private readonly ScrollableViewport scrollableViewport;

        public GameAssetBatch(ViewTransformator viewTransformator, ScrollableViewport scrollableViewport) : base()
        {
            this.fpsCalculator = new FPSCalculator(10);
            this.viewTransformator = viewTransformator;
            this.scrollableViewport = scrollableViewport;
            this.viewTransformator.PositionChanged += ChangePosition;
            this.viewTransformator.ScrollChanged += ChangeScroll;
        }

        private void ChangePosition(object? sender, EventArgs eventArgs)
        {
            characterMoving = true;
        }

        private void ChangeScroll(object? sender, EventArgs eventArgs)
        {
            scrollableViewport.Scroll((int)viewTransformator.TransformatorData.ScrollMovement.X,
                                      (int)viewTransformator.TransformatorData.ScrollMovement.Y);
            scrollMoving = true;
        }

        protected override List<GameAssetBase> InitGameAssets()
        {
            List<GameAssetBase> assets = new List<GameAssetBase>();

            var BackgroundAsset = new GameBackgroundAsset("wall", RgbaFloat.White, 2);
            BackgroundAsset.Init(0, 0, 8, 8, "wall.png");
            assets.Add(BackgroundAsset);

            // There we need different QuadGrid to place model with smaller scale
            // in the same rectangle region because coordinates are scaled too
            //var GameAsset1 = new GameSpriteAsset(RgbaFloat.Green, 1, 0.7f);
            //GameAsset1.Init(0, 7, 1, 8, "char1.png");
            //assets.Add(GameAsset1);

            var GameAsset2 = new GameSpriteAsset("character", RgbaFloat.Green, 0.5f);
            GameAsset2.Init(0, 7, 1, 8, "char2.png");
            assets.Add(GameAsset2);

            var GameAsset3 = new GameBackgroundAsset("torch", RgbaFloat.White, 1);
            GameAsset3.Init(1, 6, 2, 7, "torch.png");
            assets.Add(GameAsset3);

            var GameAsset4 = new GameBackgroundAsset("torch", RgbaFloat.White, 1);
            GameAsset4.Init(3, 6, 4, 7, "torch.png");
            assets.Add(GameAsset4);

            var GameAsset5 = new GameBackgroundAsset("torch", RgbaFloat.White, 1);
            GameAsset5.Init(5, 6, 6, 7, "torch.png");
            assets.Add(GameAsset5);

            var GameAsset6 = new GameBackgroundAsset("torch", RgbaFloat.White, 1);
            GameAsset6.Init(7, 6, 8, 7, "torch.png");
            assets.Add(GameAsset6);

            var GameFontAsset1 = new GameTextAsset("fps_info", RgbaFloat.Blue ,1);
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
                    if (e.Name == "fps_info")
                    {
                        e.SetParam(text);
                    }
                    if (e.Name == "torch")
                    {
                        var tex = e.GameTextureData as GameTextureData;
                        if (tex?.FileName == "torch2.png")
                        {
                            e.SetParam("torch.png");
                        }
                        else if (tex?.FileName == "torch.png")
                        {
                            e.SetParam("torch2.png");
                        }
                    }

                    if (characterMoving)
                    {
                        var tex = e.GameTextureData as GameTextureData;
                        if (tex?.FileName == "char2.png")
                        {
                            e.SetParam("char2_move.png");
                        }
                        else if (tex?.FileName == "char2_move.png")
                        {
                            e.SetParam("char2.png");
                        }
                    }

                    if (scrollMoving && e.AssetType == AssetType.Background)
                    {
                        var backgroundAsset = e as GameBackgroundAsset;
                        backgroundAsset?.Scroll(scrollableViewport);
                    }
                });
            }

            base.Update(delta);

            characterMoving = false;
            scrollMoving = false;
        }

        protected override void Dispose(bool disposing)
        {
            this.viewTransformator.PositionChanged -= ChangePosition;
            this.viewTransformator.ScrollChanged -= ChangeScroll;
            base.Dispose(disposing);
        }
    }
}
