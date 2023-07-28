using SandAndStonesEngine.GameTextures;
using SharpGen.Runtime;
using SkiaSharp;
using System.Runtime.InteropServices;

namespace SandAndStonesEngine.Assets
{
    public class FontTextureData : ITextureData, IDisposable
    {
        public int AssetId { get; private set; }
        public int Id { get; private set; }
        public AutoPinner PinnedImageBytes { get; private set; }
        public int BytesCount { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        private bool disposedValue;
        public FontTextureData(int assetId, int id)
        {
            this.Id = id;
            this.AssetId = assetId;
        }

        public void Init()
        {
            Update("");
        }

        private byte[] GetTextBitmap(string text = "", int colNumber = 0, int rowNumber = 0, float textSize = 10.0f, int bitmapSize = 256)
        {
            var textLinesToRender = text.Split(System.Environment.NewLine).ToList();
            byte[] bitmapBytes = null;
            var info = new SKImageInfo(bitmapSize, bitmapSize, SKColorType.Rgba8888);
            using (var surface = SKSurface.Create(info))
            {
                SKBitmap bitmap = new SKBitmap(bitmapSize, bitmapSize);
                SKCanvas canvas =  new SKCanvas(bitmap);
                canvas.Clear(SKColors.Transparent);

                SKFont font = new SKFont(SKTypeface.FromFamilyName("Arial"), textSize, 1, 0);
                using (var paint = new SKPaint(font))
                {
                    paint.TextSize = textSize;
                    paint.IsAntialias = true;
                    SKColor.TryParse("FFFF00", out SKColor color);

                    paint.Color = color;
                    paint.IsStroke = false;

                    float xTextPos = textSize * colNumber;
                    float yTextPos = textSize + textSize * rowNumber;
                    int rowCount = rowNumber;
                    foreach (var textLine in textLinesToRender)
                    {
                        yTextPos = textSize * ++rowCount;
                        canvas.DrawText(textLine, xTextPos, yTextPos, paint);
                    }
                    canvas.Flush();
                }

                SKImage img = SKImage.FromPixels(bitmap.PeekPixels());
                this.Width = img.Width;
                this.Height = img.Height;
                var pixmap = img.PeekPixels();
                var span = pixmap.GetPixelSpan();
                bitmapBytes = MemoryMarshal.AsBytes(span).ToArray();
            }

            return bitmapBytes;
        }

        public void Update(object param)
        {
            var text = param as string;
            if (text == null)
                return;
            var fontTextBytes = GetTextBitmap(text, 0, 0);
            BytesCount = fontTextBytes.Length;
            if (PinnedImageBytes != null)
            {
                PinnedImageBytes.Dispose();
            }
            PinnedImageBytes = new AutoPinner(fontTextBytes);
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

        ~FontTextureData()
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
