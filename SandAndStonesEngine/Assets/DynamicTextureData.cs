/*
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.Assets
{

    public class TextureCache
    {
        const int maxTextureCacheCapacity = 200;
        private static readonly Lazy<TextureCache> lazyInstance = new Lazy<TextureCache>(() => new TextureCache(maxTextureCacheCapacity));
        public static TextureCache Instance => lazyInstance.Value;
        int maxCapacity;

        Dictionary<string, GameTextureDataBase> textureCache;
        public TextureCache(int maxCapacity)
        {
            this.maxCapacity = maxCapacity;
        }

        public bool Add()
        {
            if (textureCache.Count >= maxCapacity )
            {
                return false;
            }

            return true;
        }

        public bool Get(string textureName, out GameTextureDataBase? textureData)
        {
            bool res = textureCache.TryGetValue(textureName, out textureData);
            if (!res)
            {
                return false;
            }

            return true;
        }
    }

    public class DynamicTextureData : GameTextureDataBase, IDisposable
    {
        public string Name { get; private set; }
        public DynamicTextureData(uint assetId, string name="") : base(assetId)
        {
            this.Name = name;
        }

        public override void Init()
        {
            Update(Name);
        }

        //public override void Update(object param imgBytes)
        //{
        //    var imageBytes = param as byte[];
        //    if (imageBytes == null)
        //    {
        //        return;
        //    }
        //    base.Update(imageBytes);
        //}

        //public byte[] GetImageBytes(byte[] imageBytes)
        //{
        //    using var image = SKImage.FromEncodedData(imageBytes);
        //    using var bitmap = SKBitmap.FromImage(image);
        //    this.Width = image.Width;
        //    this.Height = image.Height;
        //    using var pixmap = bitmap.PeekPixels();
        //    var span = pixmap.GetPixelSpan();
        //    byte[] bitmapBytes = MemoryMarshal.AsBytes(span).ToArray();
        //    return bitmapBytes;
        //}

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
*/