namespace SandAndStonesLibrary.Assets
{
    public interface IAsyncAssetReader
    {
        Task<InputAssetBatch> ReadBatchAsync();
    }
}
