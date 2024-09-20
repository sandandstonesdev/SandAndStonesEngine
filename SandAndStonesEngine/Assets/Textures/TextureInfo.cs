using SandAndStones.Shared.AssetConfig;
using Veldrid;

namespace SandAndStonesEngine.Assets.Textures
{
    public class TextureInfo : IDisposable
    {
        private bool disposedValue;

        public int Id { get; init; }
        public int TextureDataId { get; set; }
        public string Name { get; set; }
        public RgbaFloat Color { get; init; }
        public TextureType TextureType { get; init; }
        public GameTextureDataBase GameTextureData { get; set; } = null!;

        private TextureInfo(int textureDataId, string name, RgbaFloat color, AssetType assetType)
        {
            TextureDataId = textureDataId;
            Id = IdManager.GetTextureId();
            Name = name;
            Color = color;
            TextureType = (int)assetType >= 8 ? TextureType.Text : TextureType.Standard;
        }

        public void SetTextureData(GameTextureDataBase gameTextureData)
        {
            GameTextureData = gameTextureData;
        }

        public void Update(string name)
        {
            Name = name;
            GameTextureData.Update(name);
        }

        public static TextureInfo CreateTextureInfo(int textureDataId, string name, RgbaFloat color, AssetType assetType)
        {
            return new TextureInfo(textureDataId, name, color, assetType);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                GameTextureData?.Dispose();
                disposedValue = true;
            }
        }

        ~TextureInfo()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
