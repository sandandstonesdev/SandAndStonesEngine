using Veldrid;
using SixLabors.ImageSharp.Memory;
using SandAndStonesEngine.GraphicAbstractions;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp.Advanced;
using SandAndStonesEngine.GameFactories;
using System.Numerics;
using SandAndStonesEngine.Assets;

namespace SandAndStonesEngine.GameTextures
{
    public class GameTextureSurface // Asociated with SurfaceSampler
    {
        public int Id;
        public TextureView TextureView;
        private Texture Texture;

        public ResourceLayout TextureLayout;
        public ResourceSet ResourceSet;
        int bitDepth = 24;
        int width;
        int height;
        string fileName;
        AutoPinner imageBytesPinner;

        GameGraphicDevice gameGraphicDevice;

        List<ITextureData> textureDataList;
        public GameTextureSurface(List<ITextureData> textureDataList, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.textureDataList = textureDataList;
        }

        public void Init()
        {
            gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
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

        private void GetAllTextureData()
        {

        }

        public void UpdateTextureArray(uint baseLayerId = 0)
        {
            uint layerIdx = baseLayerId;
            foreach (var texData in textureDataList)
            {
                gameGraphicDevice.GraphicsDevice.UpdateTexture(Texture, texData.PinnedImageBytes, (uint)texData.BytesCount, 0, 0, 0, (uint)texData.Width, (uint)texData.Height, 1, 0, layerIdx++);
            }
        }

        public void Destroy()
        {
            TextureView.Dispose();
            Texture.Dispose();
            imageBytesPinner.Dispose();
        }
    }
}
