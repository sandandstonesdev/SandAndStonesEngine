using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MemoryStore;
using Veldrid;

namespace SandAndStonesEngine.GameTextures
{
    public class GameTextureSurface : IDisposable
    {
        // Associated with SurfaceSampler
        public int Id;
        public TextureView TextureView;
        private Texture Texture;

        private ResourceLayout textureLayout;
        public ResourceLayout TextureLayout
        {
            get { return textureLayout; }
            private set { textureLayout = value; }
        }
        private ResourceSet resourceSet;
        public ResourceSet ResourceSet
        {
            get { return resourceSet; }
            private set { resourceSet = value; }
        }

        private readonly int width;
        private readonly int height;

        private readonly AssetMemoryStore assetMemoryStore;
        private bool disposedValue;
        private readonly GameGraphicDevice graphicDevice;

        public GameTextureSurface(AssetMemoryStore assetMemoryStore, GameGraphicDevice graphicDevice, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.assetMemoryStore = assetMemoryStore;
            this.graphicDevice = graphicDevice;
        }

        public void Init()
        {
            var texDesc = new TextureDescription()
            {
                Width = (uint)width,
                Height = (uint)height,
                Depth = 1,
                MipLevels = 1,
                ArrayLayers = (uint)assetMemoryStore.GetTextureCount(),
                Format = PixelFormat.B8_G8_R8_A8_UNorm,
                Usage = TextureUsage.Sampled,
                Type = Veldrid.TextureType.Texture2D,
            };

            ResourceFactory factory = graphicDevice.ResourceFactory;

            Texture = factory.CreateTexture(texDesc);
            TextureView = factory.CreateTextureView(Texture);

            TextureLayout = factory.CreateResourceLayout(
                    new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("SurfaceTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment),
                    new ResourceLayoutElementDescription("SurfaceSampler", ResourceKind.Sampler, ShaderStages.Fragment))
                    );

            ResourceSet = factory.CreateResourceSet(new ResourceSetDescription(
                TextureLayout,
                TextureView,
                graphicDevice.GraphicsDevice.Aniso4xSampler));
        }
        public void Update()
        {
            foreach (var texture in assetMemoryStore.GetTextureData())
            {
                graphicDevice.GraphicsDevice.UpdateTexture(Texture, texture.PinnedImageBytes, (uint)texture.BytesCount, 0, 0, 0, (uint)texture.Width, (uint)texture.Height, 1, 0, (uint)texture.Id);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                TextureView.Dispose();
                Texture.Dispose();
                disposedValue = true;
            }
        }

        ~GameTextureSurface()
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
