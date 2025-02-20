using System.Text.Json;

namespace SandAndStonesLibrary.Assets
{
    public class InputAssetReader(string fileName) : IAsyncAssetReader
    {
        
        public async Task<InputAssetBatch> ReadBatchAsync()
        {
            using FileStream openStream = File.OpenRead(fileName);
            var inputAssetBatch =
                await JsonSerializer.DeserializeAsync<InputAssetBatch>(openStream,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

            return inputAssetBatch;
        }
    }
}
