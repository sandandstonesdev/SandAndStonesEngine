using SandAndStonesEngine.GameTextures;
using SkiaSharp;
using System.Runtime.InteropServices;

namespace SandAndStonesEngine.Assets
{
    public class FontTextureData : ITextureData, IDisposable
    {
        public int Id;
        public AutoPinner PinnedImageBytes { get; private set; }
        public int BytesCount { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public string text = "ABCDEFGHIJKLMNOPRSTUVWXYZ0123456789";
        private bool disposedValue;

        public FontTextureData(int id, string fileName)
        {
            this.Id = id;
        }

        public void Init()
        {
            var fontTextBytes = GetTextBitmap();
            BytesCount = fontTextBytes.Length;
            PinnedImageBytes = new AutoPinner(fontTextBytes);
        }

        private byte[] GetTextBitmap(float textSize = 10.0f, int bitmapSize = 256)
        {
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
                    SKColor.TryParse("FFFFFF", out SKColor color);

                    paint.Color = color;
                    paint.IsStroke = false;

                    canvas.DrawText(text, 0, textSize * 2, paint);
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
