using SandAndStones.Shared.AssetConfig;
using System.Diagnostics;
using System.Text.Json;

namespace SandAndStonesEngine.Clients
{
    public class InputAssetServiceReader : IAsyncAssetReader
    {
        private readonly string _assetServiceAddress = "http://localhost:42000/";
        private readonly string _path;
        private readonly HttpClient _httpClient;
        public InputAssetServiceReader(HttpClient httpClient, string path)
        {
            _httpClient = httpClient;
            _path = _assetServiceAddress + path;
        }

        public async Task<InputAssetBatch> ReadBatchAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_path}");

                response.EnsureSuccessStatusCode();

                var obj = await response.Content.ReadAsStringAsync();

                var assetInfo = JsonSerializer.Deserialize<InputAssetBatch>(obj, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return assetInfo ?? new InputAssetBatch();
            }
            catch (HttpRequestException exc)
            {
                Debug.WriteLine($"Asset info connection error: {exc.Message}");
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Asset info error: {exc.Message}");
            }

            return new InputAssetBatch();
        }
    }
}
