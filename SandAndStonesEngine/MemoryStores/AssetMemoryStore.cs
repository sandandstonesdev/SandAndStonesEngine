using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesLibrary.AssetConfig;

namespace SandAndStonesEngine.MemoryStore
{
    public class AssetMemoryStore
    {
        public readonly List<GameAssetBase> Assets = [];
        public readonly QuadModelMemoryStore quadModelMemoryStore;
        public readonly GameTextureMemoryStore gameTextureMemoryStore;

        public AssetMemoryStore(QuadModelMemoryStore quadModelMemoryStore, GameTextureMemoryStore gameTextureMemoryStore)
        {
            this.quadModelMemoryStore = quadModelMemoryStore;
            this.gameTextureMemoryStore = gameTextureMemoryStore;
        }

        public void Add(GameAssetBase asset)
        {
            Assets.Add(asset);
            if (asset.AssetBatchType == AssetBatchType.ClientRectBatch)
                quadModelMemoryStore.AddRange(asset.QuadModelList);
            if (asset.AssetBatchType == AssetBatchType.StatusBarBatch)
                quadModelMemoryStore.AddStatusBarModelsRange(asset.QuadModelList);

            gameTextureMemoryStore.Add(asset.GameTextureData);
        }

        public List<IQuadModel> GetModels()
        {
            return quadModelMemoryStore.Models;
        }

        public List<IQuadModel> GetStatusBarModels()
        {
            return quadModelMemoryStore.StatusBarModels;
        }

        public List<GameTextureDataBase> GetTextureData()
        {
            return gameTextureMemoryStore.TexturesData;
        }

        public uint GetTextureCount()
        {
            return gameTextureMemoryStore.Count;
        }
    }
}
