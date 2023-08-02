using SandAndStonesEngine.GameTextures;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Memory;
using System.Runtime.InteropServices;
using Vortice.D3DCompiler;

namespace SandAndStonesEngine.Assets
{

    public class GameTextureData : GameTextureDataBase, IDisposable
    {
        public string FileName { get; private set; }
        public GameTextureData(int assetId, string fileName) : base(assetId)
        {
            this.FileName = fileName;
        }

        public override void Init()
        {
            Update(FileName);
        }

        public override void Update(object param)
        {
            var fileName = param as string;
            if (fileName == null)
                return;

            string path = GetTextureImageFilePath(fileName);
            byte[] imageBytes = GetImageBytes(fileName);

            base.Update(imageBytes);
        }

        public byte[] GetImageBytes(string fileName)
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
