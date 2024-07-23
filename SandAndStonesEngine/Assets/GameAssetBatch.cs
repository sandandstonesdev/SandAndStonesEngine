using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Managers;
using SandAndStonesEngine.MathModule;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        public override AssetBatchType AssetBatchType => AssetBatchType.ClientRectBatch;

        ViewTransformator viewTransformator;
        bool characterMoving = false;
        bool scrollMoving = false;
        private readonly ScrollableViewport scrollableViewport;

        public GameAssetBatch(ViewTransformator viewTransformator, ScrollableViewport scrollableViewport) : base()
        {
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

        protected override void InitGameAssets()
        {
            var BackgroundAsset = new GameBackgroundAsset("wall", RgbaFloat.White, AssetBatchType, 2);
            BackgroundAsset.Init(0, 0, 8, 8, "wall.png");
            AssetDataManager.Instance.Add(BackgroundAsset);

            // There we need different QuadGrid to place model with smaller scale
            // in the same rectangle region because coordinates are scaled too
            //var GameAsset1 = new GameSpriteAsset(RgbaFloat.Green, 1, 0.7f);
            //GameAsset1.Init(0, 7, 1, 8, "char1.png");
            //assets.Add(GameAsset1);

            var GameAsset2 = new GameCharacterSpriteAsset("character", RgbaFloat.Green, AssetBatchType, 0.5f);
            GameAsset2.Init(0, 7, 1, 8, "char2.png");
            GameAsset2.SetAnimation(new TextureAnimation("char2.png", "char2_move.png"));
            AssetDataManager.Instance.Add(GameAsset2);

            var GameAsset3 = new GameBackgroundAsset("torch", RgbaFloat.White, AssetBatchType, 1);
            GameAsset3.Init(1, 6, 2, 7, "torch.png");
            GameAsset3.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset3);

            var GameAsset4 = new GameBackgroundAsset("torch", RgbaFloat.White, AssetBatchType, 1);
            GameAsset4.Init(3, 6, 4, 7, "torch.png");
            GameAsset4.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset4);

            var GameAsset5 = new GameBackgroundAsset("torch", RgbaFloat.White, AssetBatchType, 1);
            GameAsset5.Init(5, 6, 6, 7, "torch.png");
            GameAsset5.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset5);

            var GameAsset6 = new GameBackgroundAsset("torch", RgbaFloat.White, AssetBatchType, 1);
            GameAsset6.Init(7, 6, 8, 7, "torch.png");
            GameAsset6.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset6);
            
            var GameFontAsset1 = new GameTextAsset("fps_info", RgbaFloat.Blue, AssetBatchType, 1);
            GameFontAsset1.Init(0, 0, 1, 1, "letters.png");
            GameFontAsset1.SetAnimation(new TextAnimation("FPS: 0"));
            AssetDataManager.Instance.Add(GameFontAsset1);
        }

        public override void Update(long delta)
        {
            FPSCalculator.Instance.AddNextDelta(delta);
            
            if (FPSCalculator.Instance.CanDoUpdate(delta))
            {
                AssetDataManager.Instance.Assets.ForEach(e =>
                {
                    if (e.Name == "fps_info")
                    {
                        e.Animate(FPSCalculator.Instance.GetFormatedResult());
                    }
                    else if (e.Name == "torch")
                    {
                        e.Animate();
                    }
                    else if (e.Name == "character")
                    {
                        if (characterMoving)
                        {
                            e.Animate();
                        }
                    }

                    if (scrollMoving && e.AssetBatchType == AssetBatchType.ClientRectBatch && e.AssetType == AssetType.Background)
                    {
                        var backgroundAsset = e as GameBackgroundAsset;
                        backgroundAsset?.Scroll(scrollableViewport);
                    }

                    if (characterMoving && e.AssetType == AssetType.CharacterSprite)
                    {
                        var characterAsset = e as GameCharacterSpriteAsset;
                        characterAsset?.Move(viewTransformator.TransformatorData.Movement);
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
