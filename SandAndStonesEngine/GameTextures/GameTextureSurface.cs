using Veldrid;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.Assets;

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
        
        int width;
        int height;

        GameGraphicDevice gameGraphicDevice;

        List<GameTextureDataBase> textureDataList;
        private bool disposedValue;
        List<GameAssetBase> gameAssets;

        public GameTextureSurface(List<GameAssetBase> gameAssets, int width, int height)
        {
            this.gameAssets = gameAssets;
            this.width = width;
            this.height = height;
            this.textureDataList = new List<GameTextureDataBase>();
            this.gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
        }
        public void Init()
        {
            foreach(var asset in gameAssets)
            {
                var textureData = asset.GameTextureData;
                if (textureDataList.All(e => e.Id != textureData.Id))
                {
                    textureDataList.Add(asset.GameTextureData);
                }
            }

            TextureDescription texDesc = new TextureDescription()
            {
                Width = (uint)width,
                Height = (uint)height,
                Depth = 1,
                MipLevels = 1,
                ArrayLayers = (uint)textureDataList.Count,
                Format = PixelFormat.B8_G8_R8_A8_UNorm,
                Usage = TextureUsage.Sampled,
                Type = TextureType.Texture2D,
            };

            ResourceFactory factory = Factory.Instance.GetResourceFactory();

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
        public void Update()
        {
            foreach (var texture in textureDataList)
            {
                gameGraphicDevice.GraphicsDevice.UpdateTexture(Texture, texture.PinnedImageBytes, (uint)texture.BytesCount, 0, 0, 0, (uint)texture.Width, (uint)texture.Height, 1, 0, (uint)texture.Id);
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
