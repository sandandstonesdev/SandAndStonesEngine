using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.GameFactories;
using System.Numerics;
using System.Text.Json.Serialization;

namespace SandAndStonesEngine.Assets.Assets
{
    public class AssetInfo
    {
        public Vector3 QuadGridCount { get; init; }
        public Vector3 StartPos { get; init; }
        public Vector3 EndPos { get; init; }
        [JsonIgnore]
        public IAnimation Animation { get; init; }
        [JsonIgnore]
        public List<TextureInfo> Textures { get; init; } = [];
        public readonly AssetFactory AssetFactory;

        public AssetInfo()
        {

        }
        private AssetInfo(AssetFactory assetFactory, Vector3 quadGridCount, int startPosX, int startPosY, int endPosX, int endPosY, IAnimation animation, TextureInfo texture)
        {
            QuadGridCount = quadGridCount;
            StartPos = new Vector3(startPosX, startPosY, 0);
            EndPos = new Vector3(endPosX, endPosY, 0);
            Animation = animation;
            AssetFactory = assetFactory;
            Textures.Add(texture);
        }

        public static AssetInfo Create2DAssetInfo(AssetFactory assetFactory, Vector3 quadGridCount, int startPosX, int startPosY, int endPosX, int endPosY, IAnimation animation, TextureInfo texture)
        {
            return new AssetInfo(assetFactory, quadGridCount, startPosX, startPosY, endPosX, endPosY, animation, texture);
        }
    }
}
