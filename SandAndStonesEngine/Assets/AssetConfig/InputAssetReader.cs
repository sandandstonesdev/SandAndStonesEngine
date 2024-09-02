using System.Text.Json;

namespace SandAndStonesEngine.Assets.AssetConfig
{
    public class InputAssetReader
    {
        private readonly string fileName;
        public InputAssetReader(string fileName)
        {
            this.fileName = fileName;
        }

        public async Task<InputAssetBatch> ReadAsync()
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
