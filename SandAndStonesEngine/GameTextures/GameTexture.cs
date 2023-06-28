using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Memory;
using SandAndStonesEngine.GraphicAbstractions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp.Advanced;
using Vortice.Win32;

namespace SandAndStonesEngine.GameTextures
{
    class AutoPinner : IDisposable
    {
        GCHandle _pinnedArray;
        public AutoPinner(Object obj)
        {
            _pinnedArray = GCHandle.Alloc(obj, GCHandleType.Pinned);
        }
        public static implicit operator IntPtr(AutoPinner ap)
        {
            return ap._pinnedArray.AddrOfPinnedObject();
        }
        public void Dispose()
        {
            _pinnedArray.Free();
        }
    }

    public class GameTexture
    {
        private Image textureImage;
        public TextureView TextureView;
        private Texture Texture;
        public ResourceLayout TextureLayout;
        public ResourceSet ResourceSet;
        int bitDepth = 24;
        string fileName;
        GameGraphicDevice gameGraphicDevice;
        AutoPinner imageBytesPinner;
        public GameTexture(string fileName, GameGraphicDevice gameGraphicDevice)
        {
            this.textureImage = GetImage(fileName);
            this.fileName = fileName;
            this.gameGraphicDevice = gameGraphicDevice;
        }

        public void Init()
        {
            TextureDescription texDesc = new TextureDescription()
            {
                Width = (uint)textureImage.Width,
                Height = (uint)textureImage.Height,
                Depth = 1,
                MipLevels = 1,
                ArrayLayers = 1,
                Format = PixelFormat.B8_G8_R8_A8_UNorm,
                Usage = TextureUsage.Sampled,
                Type = TextureType.Texture2D,
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

        private string GetTextureImageFilePath(string fileName)
        {
            const string textureImagesPath = "GameTextures\\Images";
            string basePath = Path.GetFullPath(@".");
            string imagePath = Path.Combine(basePath, textureImagesPath, fileName);
            return imagePath;
        }
        
        public Image GetImage(string filename)
        {
            var imagePath = GetTextureImageFilePath(filename);
            FileStream inputImageStream = File.OpenRead(imagePath);
            Image image = Image.Load(inputImageStream);
            return image;
        }

        public byte[] GetImageBytes()
        {
            var outputImagePath = GetTextureImageFilePath(fileName);
            using Image<Rgba32> imagePixels = Image.Load<Rgba32>(outputImagePath);
            IMemoryGroup<Rgba32> memoryGroup = imagePixels.GetPixelMemoryGroup();
            var _memoryGroup = memoryGroup.ToArray()[0];
            var pixelData = MemoryMarshal.AsBytes(_memoryGroup.Span).ToArray();
            byte[] imageBytes = pixelData;
            return imageBytes;
        }

        public void WriteTextureToOutFile(byte[] bytes)
        {
            var textureOutFilename = "wallout.bmp";
            var outputImagePath = GetTextureImageFilePath(textureOutFilename);
            using FileStream file = new FileStream(outputImagePath, FileMode.Create, System.IO.FileAccess.Write);
            file.Write(bytes);
        }

        public void UpdateTexture()
        {
            byte[] textureImageBytes = GetImageBytes();
            imageBytesPinner = new AutoPinner(textureImageBytes);
            gameGraphicDevice.GraphicsDevice.UpdateTexture(Texture, imageBytesPinner, (uint)textureImageBytes.Length, 0, 0, 0, (uint)textureImage.Width, (uint)textureImage.Height, 1, 0, 0);
        }

        public void Destroy()
        {
            TextureView.Dispose();
            Texture.Dispose();
            imageBytesPinner.Dispose();
            textureImage.Dispose();
        }
    }
}
