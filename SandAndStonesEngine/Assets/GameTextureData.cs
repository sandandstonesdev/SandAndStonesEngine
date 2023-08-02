using SandAndStonesEngine.GameTextures;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Memory;
using System.Runtime.InteropServices;
using Vortice.D3DCompiler;

namespace SandAndStonesEngine.Assets
{

    public class GameTextureData : ITextureData, IDisposable
    {
        public int AssetId { get; private set; }
        public int Id { get; private set; }
        string fileName;
        private bool disposedValue;

        public AutoPinner PinnedImageBytes { get; private set; }
        public int BytesCount { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public GameTextureData(int assetId, string fileName)
        {
            this.Id = IdManager.GetTextureId();
            this.AssetId = assetId;
            this.fileName = fileName;
        }

        public void Init()
        {
            string path = GetTextureImageFilePath(fileName);
            byte[] imageBytes = GetImageBytes();
            BytesCount = imageBytes.Length;
            PinnedImageBytes = new AutoPinner(imageBytes);
        }

        public void Update(object param)
        {
            var fileName = param as string;
            if (fileName == null)
                return;

            string path = GetTextureImageFilePath(fileName);
            byte[] imageBytes = GetImageBytes();
            BytesCount = imageBytes.Length; 
            if (PinnedImageBytes != null)
            {
                PinnedImageBytes.Dispose();
            }
            PinnedImageBytes = new AutoPinner(imageBytes);
        }

        private string GetTextureImageFilePath(string fileName)
        {
            const string textureImagesPath = "GameTextures\\Images";
            string basePath = Path.GetFullPath(@".");
            string imagePath = Path.Combine(basePath, textureImagesPath, fileName);
            return imagePath;
        }

        private Image GetImage(string filename)
        {
            var imagePath = GetTextureImageFilePath(filename);
            FileStream inputImageStream = File.OpenRead(imagePath);
            Image image = Image.Load(inputImageStream);
            this.Width = image.Width;
            this.Height = image.Height;
            return image;
        }

        public byte[] GetImageBytes()
        {
            var outputImagePath = GetTextureImageFilePath(fileName);
            using Image<Rgba32> imagePixels = Image.Load<Rgba32>(outputImagePath);
            this.Width = imagePixels.Width;
            this.Height = imagePixels.Height;
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

        ~GameTextureData()
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
