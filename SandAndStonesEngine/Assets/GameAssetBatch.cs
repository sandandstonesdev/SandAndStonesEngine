using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.Managers;
using SandAndStonesEngine.MathModule;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        public override AssetBatchType AssetBatchType => AssetBatchType.ClientRectBatch;
        public override List<IQuadModel> Assets => AssetDataManager.Instance.ModelData;

        ViewTransformator viewTransformator;
        
        public GameAssetBatch(ViewTransformator viewTransformator, ScrollableViewport scrollableViewport) : base(scrollableViewport)
        {
            this.viewTransformator = viewTransformator;
        }

        protected override void InitGameAssets()
        {
            QuadGridManager.Instance.StartNewBatch();

            var BackgroundAsset = new GameBackgroundAsset("wall", scrollableViewport, RgbaFloat.White, AssetBatchType, 2);
            BackgroundAsset.Init(0, 0, 8, 8, "wall.png");
            BackgroundAsset.SetAnimation(new TextureAnimation("wall.png"));
            AssetDataManager.Instance.Add(BackgroundAsset);

            var GameAsset2 = new GameCharacterSpriteAsset("character", viewTransformator, RgbaFloat.Green, AssetBatchType, 0.5f);
            GameAsset2.Init(0, 7, 1, 8, "char2.png");
            GameAsset2.SetAnimation(new TextureAnimation("char2.png", "char2_move.png"));
            AssetDataManager.Instance.Add(GameAsset2);

            var GameAsset3 = new GameBackgroundAsset("torch", scrollableViewport, RgbaFloat.White, AssetBatchType, 1);
            GameAsset3.Init(1, 6, 2, 7, "torch.png");
            GameAsset3.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset3);

            var GameAsset4 = new GameBackgroundAsset("torch", scrollableViewport, RgbaFloat.White, AssetBatchType, 1);
            GameAsset4.Init(3, 6, 4, 7, "torch.png");
            GameAsset4.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset4);

            var GameAsset5 = new GameBackgroundAsset("torch", scrollableViewport, RgbaFloat.White, AssetBatchType, 1);
            GameAsset5.Init(5, 6, 6, 7, "torch.png");
            GameAsset5.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset5);

            var GameAsset6 = new GameBackgroundAsset("torch", scrollableViewport, RgbaFloat.White, AssetBatchType, 1);
            GameAsset6.Init(7, 6, 8, 7, "torch.png");
            GameAsset6.SetAnimation(new TextureAnimation("torch.png", "torch2.png"));
            AssetDataManager.Instance.Add(GameAsset6);
            
            var GameFontAsset1 = new GameFPSCounterTextAsset("fps_info", RgbaFloat.Blue, AssetBatchType, 1.0f, 1.0f);
            GameFontAsset1.Init(0, 0, 1, 1, "letters.png");
            GameFontAsset1.SetAnimation(new TextAnimation("FPS: 0"));
            AssetDataManager.Instance.Add(GameFontAsset1);

            base.InitGameAssets();
        }

        public override void Update(long delta)
        {
            FPSCalculator.Instance.AddNextDelta(delta);
            
            if (FPSCalculator.Instance.CanDoUpdate(delta))
            {
                base.Update(delta);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
