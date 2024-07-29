using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.Managers;
using SandAndStonesEngine.MathModule;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
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

            var BackgroundAsset = new GameBackgroundAsset("wall", scrollableViewport, AssetBatchType, 2);
            BackgroundAsset.Init(AssetInfo.Create2DAssetInfo(0, 0, 8, 8, new TextureAnimation("wall.png"), TextureInfo.CreateTextureInfo("wall.png", RgbaFloat.White)));
            AssetDataManager.Instance.Add(BackgroundAsset);

            var GameAsset2 = new GameCharacterSpriteAsset("character", viewTransformator, AssetBatchType, 0.5f);
            GameAsset2.Init(AssetInfo.Create2DAssetInfo(0, 7, 1, 8, new TextureAnimation("char2.png", "char2_move.png"), TextureInfo.CreateTextureInfo("char2.png", RgbaFloat.Green)));
            AssetDataManager.Instance.Add(GameAsset2);

            var GameAsset3 = new GameBackgroundAsset("torch", scrollableViewport, AssetBatchType, 1);
            GameAsset3.Init(AssetInfo.Create2DAssetInfo(1, 6, 2, 7, new TextureAnimation("torch.png", "torch2.png"), TextureInfo.CreateTextureInfo("torch.png", RgbaFloat.White)));
            AssetDataManager.Instance.Add(GameAsset3);

            var GameAsset4 = new GameBackgroundAsset("torch", scrollableViewport, AssetBatchType, 1);
            GameAsset4.Init(AssetInfo.Create2DAssetInfo(3, 6, 4, 7, new TextureAnimation("torch.png", "torch2.png"), TextureInfo.CreateTextureInfo("torch.png", RgbaFloat.White)));
            AssetDataManager.Instance.Add(GameAsset4);

            var GameAsset5 = new GameBackgroundAsset("torch", scrollableViewport, AssetBatchType, 1);
            GameAsset5.Init(AssetInfo.Create2DAssetInfo(5, 6, 6, 7, new TextureAnimation("torch.png", "torch2.png"), TextureInfo.CreateTextureInfo("torch.png", RgbaFloat.White)));
            AssetDataManager.Instance.Add(GameAsset5);

            var GameAsset6 = new GameBackgroundAsset("torch", scrollableViewport, AssetBatchType, 1);
            GameAsset6.Init(AssetInfo.Create2DAssetInfo(7, 6, 8, 7, new TextureAnimation("torch.png", "torch2.png"), TextureInfo.CreateTextureInfo("torch.png", RgbaFloat.White)));
            AssetDataManager.Instance.Add(GameAsset6);

            var GameFontAsset1 = new GameFPSCounterTextAsset("fps_info", AssetBatchType, 1.0f, 1.0f);
            GameFontAsset1.Init(AssetInfo.Create2DAssetInfo(0, 0, 1, 1, new TextAnimation("FPS: 0"), TextureInfo.CreateTextureInfo("letters.png", RgbaFloat.Blue)));
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
