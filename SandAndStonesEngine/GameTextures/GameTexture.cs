﻿//using Veldrid;
//using SixLabors.ImageSharp.Memory;
//using SandAndStonesEngine.GraphicAbstractions;
//using System.Runtime.InteropServices;
//using SixLabors.ImageSharp.Advanced;
//using SandAndStonesEngine.GameFactories;
//using System.Numerics;
//using SandAndStonesEngine.Assets;

//namespace SandAndStonesEngine.GameTextures
//{
//    public class GameTextureSurface : IDisposable
//    {
//        // Associated with SurfaceSampler
//        public int Id;
//        public TextureView TextureView;
//        private Texture Texture;

//        public ResourceLayout TextureLayout;
//        public ResourceSet ResourceSet;
//        int bitDepth = 24;
//        int width;
//        int height;
//        string fileName;

//        GameGraphicDevice gameGraphicDevice;

//        List<ITextureData> textureDataList;
//        private bool disposedValue;

//        public GameTextureSurface(int width, int height)
//        {
//            this.width = width;
//            this.height = height;
//            this.textureDataList = new List<ITextureData>();
//        }

//        public void Init()
//        {
//            gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
//            TextureDescription texDesc = new TextureDescription()
//            {
//                Width = (uint)width,
//                Height = (uint)height,
//                Depth = 1,
//                MipLevels = 1,
//                ArrayLayers = (uint)6,//textureDataList.Count,
//                Format = PixelFormat.B8_G8_R8_A8_UNorm,
//                Usage = TextureUsage.Sampled,
//                Type = TextureType.Texture2D,
//            };

//            ResourceFactory factory = Factory.Instance.GetResourceFactory();

//            Texture = factory.CreateTexture(texDesc);
//            TextureView = factory.CreateTextureView(Texture);

//            TextureLayout = factory.CreateResourceLayout(
//                    new ResourceLayoutDescription(
//                    new ResourceLayoutElementDescription("SurfaceTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment),
//                    new ResourceLayoutElementDescription("SurfaceSampler", ResourceKind.Sampler, ShaderStages.Fragment))
//                    );

//            ResourceSet = factory.CreateResourceSet(new ResourceSetDescription(
//                TextureLayout,
//                TextureView,
//                gameGraphicDevice.GraphicsDevice.Aniso4xSampler));


//        }

//        public void AddToTextureDataList(ITextureData textureData)
//        {
//            textureDataList.Add(textureData);
//        }
//        public void UpdateTextureArray(uint baseLayerId = 0, uint lastLayer=3)
//        {
//            for (uint layerIdx = baseLayerId; layerIdx <= lastLayer; layerIdx++)
//            {
//                var texData = textureDataList[(int)layerIdx];
//                gameGraphicDevice.GraphicsDevice.UpdateTexture(Texture, texData.PinnedImageBytes, (uint)texData.BytesCount, 0, 0, 0, (uint)texData.Width, (uint)texData.Height, 1, 0, layerIdx);
//            }
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                if (disposing)
//                {
//                }

//                TextureView.Dispose();
//                Texture.Dispose();
//                disposedValue = true;
//            }
//        }

//        ~GameTextureSurface()
//        {
//            Dispose(disposing: false);
//        }

//        public void Dispose()
//        {
//            Dispose(disposing: true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}
