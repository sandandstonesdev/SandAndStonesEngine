namespace SandAndStonesLibrary.AssetConfig
{
    public interface IAsyncAssetReader
    {
        Task<InputAssetBatch> ReadBatchAsync();
    }
}
