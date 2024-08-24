using System.Diagnostics;
using System.Text.Json;

namespace SandAndStonesEngine.Clients
{
    public class AssetInfoClient
    {
        private readonly string _assetServiceAddress = "http://localhost:4200/assetinfo";
        private readonly HttpClient _httpClient;
        public AssetInfoClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Vector3DTO>> GetAssetInfo()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_assetServiceAddress}");

                response.EnsureSuccessStatusCode();

                var obj = await response.Content.ReadAsStringAsync();

                var assetInfo = JsonSerializer.Deserialize<List<Vector3DTO>>(obj, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return assetInfo ?? [];
            }
            catch (HttpRequestException exc)
            {
                Debug.WriteLine($"Asset info connection error: {exc.Message}");
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Asset info error: {exc.Message}");
            }

            return [];
        }
    }
}
