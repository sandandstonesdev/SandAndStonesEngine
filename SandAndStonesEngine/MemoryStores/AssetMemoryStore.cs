using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStones.Shared.AssetConfig;

namespace SandAndStonesEngine.MemoryStore
{
    public class AssetMemoryStore
    {
        public readonly List<GameAssetBase> Assets = [];
        public readonly QuadModelMemoryStore quadModelMemoryStore;
        public readonly GameTextureInfoStore gameTextureInfoStore;

        public AssetMemoryStore(QuadModelMemoryStore quadModelMemoryStore, GameTextureInfoStore gameTextureInfoStore)
        {
            this.quadModelMemoryStore = quadModelMemoryStore;
            this.gameTextureInfoStore = gameTextureInfoStore;
        }

        public void Add(GameAssetBase asset)
        {
            Assets.Add(asset);
            if (asset.AssetBatchType == AssetBatchType.ClientRectBatch)
                quadModelMemoryStore.AddRange(asset.QuadModelList);
            if (asset.AssetBatchType == AssetBatchType.StatusBarBatch)
                quadModelMemoryStore.AddStatusBarModelsRange(asset.QuadModelList);
        }

        public List<IQuadModel> GetModels()
        {
            return quadModelMemoryStore.Models;
        }

        public List<IQuadModel> GetStatusBarModels()
        {
            return quadModelMemoryStore.StatusBarModels;
        }


        public IReadOnlyCollection<GameTextureDataBase> GetTextureData()
        {
            var texturesData = new List<GameTextureDataBase>();
            foreach (var tex in gameTextureInfoStore.TexturesInfo)
            {
                texturesData.Add(tex.Value.GameTextureData);
            }
            return texturesData;
        }

        public uint GetTextureCount()
        {
            return gameTextureInfoStore.Count;
        }
    }
}
