using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Managers;
using SandAndStonesEngine.MathModule;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        public override AssetBatchType AssetBatchType => AssetBatchType.ClientRectBatch;
        public override List<IQuadModel> Assets => AssetDataManager.Instance.ModelData;

        private readonly ViewTransformator viewTransformator;

        private readonly QuadGridManager quadGridManager;

        public GameAssetBatch(GameGraphicDevice graphicDevice, QuadGridManager quadGridManager, ViewTransformator viewTransformator, ScrollableViewport scrollableViewport) : base(graphicDevice, scrollableViewport)
        {
            this.quadGridManager = quadGridManager;
            this.viewTransformator = viewTransformator;
        }

        protected override async void InitGameAssets()
        {
            quadGridManager.StartNewBatch();

            //var client = new AssetInfoClient(new HttpClient());
            //var assetInfo = client.GetAssetInfo().Result;

            var BackgroundAsset = new GameBackgroundAsset("wall", viewTransformator, scrollableViewport, AssetBatchType, 2);
            BackgroundAsset.Init(quadGridManager, AssetInfo.Create2DAssetInfo(quadGridManager.QuadCount, 0, 0, 16, 8, new TextureAnimation("wall.png"), TextureInfo.CreateTextureInfo("wall.png", RgbaFloat.White)));
            AssetDataManager.Instance.Add(BackgroundAsset);

            var GameAsset2 = new GameCharacterSpriteAsset("character", viewTransformator, AssetBatchType, 0.5f);
            GameAsset2.Init(quadGridManager, AssetInfo.Create2DAssetInfo(quadGridManager.QuadCount, 0, 7, 1, 8, new TextureAnimation("char2.png", "char2_move.png"), TextureInfo.CreateTextureInfo("char2.png", RgbaFloat.Green)));
            AssetDataManager.Instance.Add(GameAsset2);

            for (int i = 1; i < 16; i += 2)
            {
                var GameAsset3 = new GameBackgroundAsset("torch", viewTransformator, scrollableViewport, AssetBatchType, 1);
                GameAsset3.Init(quadGridManager, AssetInfo.Create2DAssetInfo(quadGridManager.QuadCount, i, 6, i + 1, 7, new TextureAnimation("torch.png", "torch2.png"), TextureInfo.CreateTextureInfo("torch.png", RgbaFloat.White)));
                AssetDataManager.Instance.Add(GameAsset3);
            }

            var GameFontAsset1 = new GameFPSCounterTextAsset("fps_info", AssetBatchType, 1.0f, 1.0f);
            GameFontAsset1.Init(quadGridManager, AssetInfo.Create2DAssetInfo(quadGridManager.QuadCount, 0, 0, 1, 1, new TextAnimation("FPS: 0"), TextureInfo.CreateTextureInfo("letters.png", RgbaFloat.Blue)));
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
