using SandAndStonesEngine.GameTextures;

namespace SandAndStonesEngine.Assets.Textures
{
    public abstract class GameTextureDataBase
    {
        public uint AssetId { get; private set; }
        public int Id { get; private set; }
        public abstract TextureType Type { get; }

        private bool disposedValue;

        public AutoPinner PinnedImageBytes { get; protected set; }
        public int BytesCount { get; protected set; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public GameTextureDataBase(int id, uint assetId)
        {
            Id = id;
            AssetId = assetId;
        }

        public abstract void Init();

        protected AutoPinner PinImageBytes(byte[] imageBytes)
        {
            if (PinnedImageBytes != null)
            {
                PinnedImageBytes.Dispose();
            }
            return new AutoPinner(imageBytes);
        }

        public virtual void Update(object param)
        {
            var imageBytes = param as byte[];
            if (imageBytes == null)
            {
                return;
            }

            BytesCount = imageBytes.Length;
            PinnedImageBytes = PinImageBytes(imageBytes);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                PinnedImageBytes.Dispose();
                disposedValue = true;
            }
        }

        ~GameTextureDataBase()
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
