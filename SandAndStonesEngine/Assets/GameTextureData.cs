using SkiaSharp;
using System.Runtime.InteropServices;

namespace SandAndStonesEngine.Assets
{

    public class GameTextureData : GameTextureDataBase, IDisposable
    {
        public string FileName { get; private set; }
        public GameTextureData(uint assetId, string fileName) : base(assetId)
        {
            this.FileName = fileName;
        }

        public override void Init()
        {
            Update(FileName);
        }

        public override void Update(object param)
        {
            FileName = param as string;
            if (FileName == null)
                return;

            string path = GetTextureImageFilePath(FileName);
            byte[] imageBytes = GetImageBytes(FileName);

            base.Update(imageBytes);
        }

        public byte[] GetImageBytes(string fileName)
        {
            var outputImagePath = GetTextureImageFilePath(fileName);
            using var image = SKImage.FromEncodedData(outputImagePath);
            using var bitmap = SKBitmap.FromImage(image);
            this.Width = image.Width;
            this.Height = image.Height;
            using var pixmap = bitmap.PeekPixels();
            var span = pixmap.GetPixelSpan();
            byte[] bitmapBytes = MemoryMarshal.AsBytes(span).ToArray();
            return bitmapBytes;
        }

        public string GetTextureImageFilePath(string fileName)
        {
            const string textureImagesPath = "GameTextures\\Images";
            string basePath = Path.GetFullPath(@".");
            string imagePath = Path.Combine(basePath, textureImagesPath, fileName);
            return imagePath;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
