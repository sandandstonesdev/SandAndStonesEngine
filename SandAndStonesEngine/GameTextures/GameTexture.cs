using Veldrid;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Assets.Textures;

namespace SandAndStonesEngine.GameTextures
{
    public class GameTexture: IDisposable
    {
        // Associated with SurfaceSampler
        public int Id;
        public TextureView TextureView;
        private Texture Texture;

        public ResourceLayout TextureLayout;
        public ResourceSet ResourceSet;
        
        private readonly int width;
        private readonly int height;
        
        private readonly GameGraphicDevice gameGraphicDevice;

        private readonly List<ITextureData> textureDataList;
        private bool disposedValue;

        public GameTexture(int width, int height, GameGraphicDevice graphicDevice)
        {
            this.width = width;
            this.height = height;
            this.textureDataList = [];
            this.gameGraphicDevice = graphicDevice;
        }

        public void Init()
        {
            //gameGraphicDevice = grap;
            TextureDescription texDesc = new()
            {
                Width = (uint)width,
                Height = (uint)height,
                Depth = 1,
                MipLevels = 1,
                ArrayLayers = (uint)6,//textureDataList.Count,
                Format = PixelFormat.B8_G8_R8_A8_UNorm,
                Usage = TextureUsage.Sampled,
                Type = Veldrid.TextureType.Texture2D,
            };

            ResourceFactory factory = gameGraphicDevice.ResourceFactory;

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
                gameGraphicDevice.GraphicsDevice.Aniso4xSampler));


        }

        public void AddToTextureDataList(ITextureData textureData)
        {
            textureDataList.Add(textureData);
        }
        public void UpdateTextureArray(uint baseLayerId = 0, uint lastLayer = 3)
        {
            for (uint layerIdx = baseLayerId; layerIdx <= lastLayer; layerIdx++)
            {
                var texData = textureDataList[(int)layerIdx];
                gameGraphicDevice.GraphicsDevice.UpdateTexture(Texture, texData.PinnedImageBytes, (uint)texData.BytesCount, 0, 0, 0, (uint)texData.Width, (uint)texData.Height, 1, 0, layerIdx);
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

        ~GameTexture()
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
