using SkiaSharp;
using System.Runtime.InteropServices;

namespace SandAndStonesEngine.Assets.Textures
{
    public class FontTextureData : GameTextureDataBase, IDisposable
    {
        public override TextureType Type => TextureType.Text;
        public FontTextureData(int id, uint assetId) : base(id, assetId)
        {
        }

        public override void Init()
        {
            Update("");
        }

        public override void Update(object param)
        {
            var text = param as string;
            if (text == null)
                return;
            var fontTextBytes = GetTextBitmap(text, 0, 0);
            base.Update(fontTextBytes);
        }

        public byte[] GetTextBitmap(string text = "", int colNumber = 0, int rowNumber = 0, float textSize = 50.0f, int bitmapSize = 256)
        {
            var textLinesToRender = text.Split(Environment.NewLine).ToList();
            using var bitmap = new SKBitmap(bitmapSize, bitmapSize);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.Transparent);

            using var font = new SKFont(SKTypeface.FromFamilyName("Arial"), textSize, 1, 0);
            using (var paint = new SKPaint(font))
            {
                paint.TextSize = textSize;
                paint.IsAntialias = true;
                SKColor.TryParse("FFFFFF", out SKColor color);

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

            Width = bitmap.Width;
            Height = bitmap.Height;
            var pixmap = bitmap.PeekPixels();
            var span = pixmap.GetPixelSpan();
            byte[] bitmapBytes = MemoryMarshal.AsBytes(span).ToArray();

            return bitmapBytes;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
