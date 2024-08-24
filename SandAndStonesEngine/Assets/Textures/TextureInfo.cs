using Veldrid;

namespace SandAndStonesEngine.Assets.Textures
{
    public class TextureInfo
    {
        public string Name { get; init; }
        public RgbaFloat Color { get; init; }

        private TextureInfo(string name, RgbaFloat color)
        {
            Name = name;
            Color = color;
        }

        public static TextureInfo CreateTextureInfo(string name, RgbaFloat color)
        {
            return new TextureInfo(name, color);
        }
    }
}
