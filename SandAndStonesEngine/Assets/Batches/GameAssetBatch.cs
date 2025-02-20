using SandAndStonesEngine.Assets.DTOs;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.MemoryStores;
using SandAndStonesLibrary.AssetConfig;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public class GameAssetBatch : GameAssetBatchBase
    {
        public override AssetBatchType AssetBatchType => AssetBatchType.ClientRectBatch;
        public override List<IQuadModel> Assets => assetMemoryStore.GetModels();

        private readonly ViewTransformator viewTransformator;

        private readonly QuadGridManager quadGridManager;
        private readonly GameTextureMemoryStore gameTextureMemoryStore;

        public GameAssetBatch(AssetFactory assetFactory, AssetMemoryStore assetMemoryStore, GameTextureMemoryStore gameTextureMemoryStore, GameGraphicDevice graphicDevice, QuadGridManager quadGridManager, ViewTransformator viewTransformator, ScrollableViewport scrollableViewport) : base(assetFactory, assetMemoryStore, graphicDevice, scrollableViewport)
        {
            this.quadGridManager = quadGridManager;
            this.viewTransformator = viewTransformator;
            this.gameTextureMemoryStore = gameTextureMemoryStore;
        }

        protected override async void InitGameAssets()
        {
            quadGridManager.StartNewBatch();

            var assetsReader = assetFactory.CreateAssetReader("assets.json", false);
            var assetsData = assetsReader.ReadBatchAsync().Result;

            foreach (var assetData in assetsData.InputAssets)
            {
                Vector4 startPos = assetData.StartPos;
                Vector4 endPos = assetData.EndPos;

                for (int i = 0; i < assetData.Instances; i++)
                {
                    assetMemoryStore.Add(assetFactory.CreateGameAsset
                    (
                        assetData.Name,
                        new AssetPosInfo(startPos, endPos),
                        assetFactory, scrollableViewport, quadGridManager,
                        viewTransformator,
                        assetData.AssetBatchType,
                        assetData.AssetType,
                        assetData.Text,
                        assetData.AnimationTextureFiles,
                        assetData.Depth,
                        assetData.Scale,
                        gameTextureMemoryStore.GetTextureInfo(assetData.AnimationTextureFiles[0], new RgbaFloat(assetData.Color), assetData.Id, assetData.AssetType))
                    );

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
