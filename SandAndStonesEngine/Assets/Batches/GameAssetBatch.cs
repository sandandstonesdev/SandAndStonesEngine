using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.DTOs;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.Clients;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Managers;
using SandAndStonesEngine.MathModule;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        public override AssetBatchType AssetBatchType => AssetBatchType.ClientRectBatch;
        public override List<IQuadModel> Assets => assetFactory.ModelData;

        private readonly ViewTransformator viewTransformator;

        private readonly QuadGridManager quadGridManager;

        public GameAssetBatch(AssetFactory assetFactory, GameGraphicDevice graphicDevice, QuadGridManager quadGridManager, ViewTransformator viewTransformator, ScrollableViewport scrollableViewport) : base(assetFactory, graphicDevice, scrollableViewport)
        {
            this.quadGridManager = quadGridManager;
            this.viewTransformator = viewTransformator;
        }

        protected override async void InitGameAssets()
        {
            quadGridManager.StartNewBatch();

            //var client = new AssetInfoClient(new HttpClient());
            //var assetInfo = client.GetAssetInfo().Result;

            assetFactory.Add(assetFactory.CreateGameAsset("wall", new AssetPosInfo(new Vector2(0, 0), new Vector2(16, 8)), assetFactory, scrollableViewport, quadGridManager, viewTransformator, AssetBatchType, AssetType.Background, RgbaFloat.White, string.Empty, ["wall.png"], 2, 1));
            assetFactory.Add(assetFactory.CreateGameAsset("character", new AssetPosInfo(new Vector2(0, 7), new Vector2(1, 8)), assetFactory, scrollableViewport, quadGridManager, viewTransformator, AssetBatchType, AssetType.CharacterSprite, RgbaFloat.Green, string.Empty,["char2.png", "char2_move.png"], 0.5f, 1));

            for (int i = 1; i < 16; i += 2)
            {
                assetFactory.Add(assetFactory.CreateGameAsset("torch", new AssetPosInfo(new Vector2(i, 6), new Vector2(i + 1, 7)), assetFactory, scrollableViewport, quadGridManager, viewTransformator, AssetBatchType, AssetType.Background, RgbaFloat.White, string.Empty, ["torch.png", "torch2.png"], 1, 1));
            }
            
            assetFactory.Add(assetFactory.CreateGameAsset("fps_info", new AssetPosInfo(new Vector2(0, 0), new Vector2(1, 1)), assetFactory, scrollableViewport, quadGridManager, viewTransformator, AssetBatchType, AssetType.FPSText, RgbaFloat.Blue, "FPS: 0", ["letters.png"], 1.0f, 1.0f));

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
