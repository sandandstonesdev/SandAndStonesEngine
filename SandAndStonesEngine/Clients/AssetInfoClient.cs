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

                var serializeOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };

                var assetInfo = JsonSerializer.Deserialize<List<Vector3DTO>>(obj, serializeOptions);

                return assetInfo ?? [];
            }
            catch(HttpRequestException exc)
            {
                Debug.WriteLine($"Asset info connection error.");
            }
            catch (Exception exc)
            {
                Debug.WriteLine($"Asset info error.");
            }

            return [];
        }
    }
}
