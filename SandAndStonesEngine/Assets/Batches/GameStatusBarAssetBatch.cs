using SandAndStonesEngine.Assets.DTOs;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MemoryStore;
using SandAndStonesLibrary.AssetConfig;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public class GameStatusBarAssetBatch : GameAssetBatchBase
    {
        public override List<IQuadModel> Assets => assetMemoryStore.GetStatusBarModels();
        public override AssetBatchType AssetBatchType => AssetBatchType.StatusBarBatch;

        public readonly QuadGridManager quadGridManager;
        private readonly GameTextureInfoStore gameTextureInfoStore;

        public GameStatusBarAssetBatch(AssetFactory assetFactory, AssetMemoryStore assetMemoryStore, GameTextureInfoStore gameTextureInfoStore, GameGraphicDevice graphicDevice, QuadGridManager quadGridManager, ScrollableViewport scrollableViewport) : base(assetFactory, assetMemoryStore, graphicDevice, scrollableViewport)
        {
            this.quadGridManager = quadGridManager;
            this.gameTextureInfoStore = gameTextureInfoStore;
        }

        protected async override void InitGameAssets()
        {
            quadGridManager.StartNewBatch();

            var assetsReader = assetFactory.CreateAssetReader("assetBatch/1", true);
            var assetsData = assetsReader.ReadBatchAsync().Result;

            foreach (var assetData in assetsData.InputAssets)
            {
                assetMemoryStore.Add(assetFactory.CreateGameAsset(
                        assetData.Name,
                        new AssetPosInfo(assetData.StartPos, assetData.EndPos),
                        assetFactory, scrollableViewport, quadGridManager,
                        null!,
                        assetData.AssetBatchType,
                        assetData.AssetType,
                        assetData.Text,
                        assetData.AnimationTextureFiles,
                        assetData.Depth,
                        assetData.Scale,
                        gameTextureInfoStore.GetTextureInfo(assetData.AnimationTextureFiles[0], new RgbaFloat(assetData.Color), assetData.Id, assetData.AssetType))
                    );
            }

            base.InitGameAssets();
        }

        public override void Update(long delta)
        {
            base.Update(delta);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
