using SandAndStonesEngine.Assets;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.GameFactories;
using SandAndStonesLibrary.AssetConfig;
using Veldrid;

namespace SandAndStonesEngine.MemoryStores
{
    public class GameTextureMemoryStore
    {
        private readonly AssetFactory assetFactory;
        public uint Count { get; private set; }
        public Dictionary<string, TextureInfo> TexturesInfo { get; } = [];

        public GameTextureMemoryStore(AssetFactory assetFactory)
        {
            this.assetFactory = assetFactory;
        }

        public TextureInfo GetTextureInfo(string name, RgbaFloat color, int assetId, AssetType assetType)
        {
            const string fontTextureName = "letters.png";
            TextureInfo texInfo = null!;
            if (!TexturesInfo.ContainsKey(name))
            {
                texInfo = CreateTextureInfo(name, color, assetId, assetType);
            }
            else
            {
                texInfo = name != fontTextureName ?
                    TexturesInfo[name] : CreateTextureInfo($"{name} {Guid.NewGuid().ToString()}", color, assetId, assetType);
            }

            return texInfo;
        }

        private TextureInfo CreateTextureInfo(string name, RgbaFloat color, int assetId, AssetType assetType)
        {
            var texInfo = TextureInfo.CreateTextureInfo(IdManager.GetTextureDataId(), name, color, assetType);
            texInfo.SetTextureData(assetFactory.CreateTexture(texInfo.TextureDataId, (uint)assetId, name, texInfo.TextureType));
            TexturesInfo.Add(name, texInfo);
            ++Count;
            return texInfo;
        }
    }
}