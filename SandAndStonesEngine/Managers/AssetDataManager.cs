using SandAndStonesEngine.Assets.Assets;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Quads;

namespace SandAndStonesEngine.Managers
{
    public class AssetDataManager
    {
        private static readonly Lazy<AssetDataManager> lazyInstance = new Lazy<AssetDataManager>(() => new AssetDataManager());
        public static AssetDataManager Instance => lazyInstance.Value;

        public List<GameAssetBase> Assets { get; } = new();
        public List<IQuadModel> ModelData => QuadModelManager.Instance.Models;
        public List<IQuadModel> StatusBarModels => QuadModelManager.Instance.StatusBarModels;
        public List<GameTextureDataBase> TexturesData => GameTextureDataManager.Instance.TexturesData;

        public AssetDataManager()
        {
            
        }

        static uint lastAssetId = 0;
        public void Add(GameAssetBase asset)
        {
            Assets.Add(asset);
            if (asset.AssetBatchType == AssetBatchType.ClientRectBatch)
                QuadModelManager.Instance.AddRange(asset.QuadModelList);
            if (asset.AssetBatchType == AssetBatchType.StatusBarBatch)
                QuadModelManager.Instance.AddStatusBarModelsRange(asset.QuadModelList);
            
            GameTextureDataManager.Instance.Add(asset.GameTextureData);
        }
    }
}
