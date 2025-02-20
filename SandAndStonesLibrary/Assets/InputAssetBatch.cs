using System.Text.Json.Serialization;

namespace SandAndStonesLibrary.Assets
{
    public class InputAssetBatch
    {
        [JsonRequired]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("assets")]
        public List<InputAsset> InputAssets { get; set; }
    }
}
