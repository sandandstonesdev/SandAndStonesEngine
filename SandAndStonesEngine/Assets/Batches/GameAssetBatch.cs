using SandAndStonesEngine.Assets.AssetConfig;
using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.DTOs;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.MemoryStore;
using System.Numerics;
using System.Text.Json;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        public override AssetBatchType AssetBatchType => AssetBatchType.ClientRectBatch;
        public override List<IQuadModel> Assets => assetMemoryStore.GetModels();

        private readonly ViewTransformator viewTransformator;

        private readonly QuadGridManager quadGridManager;

        public GameAssetBatch(AssetFactory assetFactory, AssetMemoryStore assetMemoryStore, GameGraphicDevice graphicDevice, QuadGridManager quadGridManager, ViewTransformator viewTransformator, ScrollableViewport scrollableViewport) : base(assetFactory, assetMemoryStore, graphicDevice, scrollableViewport)
        {
            this.quadGridManager = quadGridManager;
            this.viewTransformator = viewTransformator;
        }

        protected override async void InitGameAssets()
        {
            quadGridManager.StartNewBatch();

            var assetsReader = new InputAssetReader("./Assets/AssetConfig/assets.json");
            var assetsData = await assetsReader.ReadAsync();
            //var client = new AssetInfoClient(new HttpClient());
            //var assetInfo = client.GetAssetInfo().Result;


            foreach (var assetData in assetsData.InputAssets)
            {
                Vector4 startPos = assetData.StartPos;
                Vector4 endPos = assetData.EndPos;

                for (int i = 0; i < assetData.Instances; i++)
                {
                    assetMemoryStore.Add(assetFactory.CreateGameAsset(
                    assetData.Name,
                    new AssetPosInfo(startPos, endPos),
                    assetFactory, scrollableViewport, quadGridManager,
                    viewTransformator,
                    assetData.AssetBatchType,
                    assetData.AssetType,
                    new RgbaFloat(assetData.Color),
                    assetData.Text,
                    assetData.AnimationTextureFiles,
                    assetData.Depth,
                    assetData.Scale
                    ));

                    startPos = Vector4.Add(startPos, assetData.InstancePosOffset);
                    endPos = Vector4.Add(endPos, assetData.InstancePosOffset);
                }
            }

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
