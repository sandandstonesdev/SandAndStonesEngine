using SandAndStonesEngine.GameTextures;

namespace SandAndStonesEngine.Assets
{
    public interface ITextureData
    {
        AutoPinner PinnedImageBytes { get; }
        int BytesCount { get; }

        void Init();
        void Update(object param);
        int Width { get; }
        int Height { get; }
    }
}