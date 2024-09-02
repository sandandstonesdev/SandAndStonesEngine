using System.Text.Json.Serialization;

namespace SandAndStonesEngine.Assets.AssetConfig
{
    public  class InputAssetBatch
    {
        [JsonPropertyName("assets")]
        public List<InputAsset> InputAssets { get; set; }
    }
}
