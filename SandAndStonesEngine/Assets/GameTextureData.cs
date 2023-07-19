using SandAndStonesEngine.GameTextures;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Memory;
using System.Runtime.InteropServices;
using Vortice.D3DCompiler;

namespace SandAndStonesEngine.Assets
{
    public class GameTextureData
    {
        public int Id;
        string fileName;
        //private Image textureImage;
        public AutoPinner PinnedImageBytes;
        public int BytesCount;
        public GameTextureData(int id, string fileName)
        {
            this.Id = id;
            this.fileName = fileName;
            //this.textureImage = GetImage(fileName);
        }

        public void Init()
        {
            string path = GetTextureImageFilePath(fileName);
            byte[] imageBytes = GetImageBytes();
            BytesCount = imageBytes.Length;
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

        public void Destroy()
        {
            PinnedImageBytes.Dispose();
        }
    }
}
