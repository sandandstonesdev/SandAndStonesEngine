using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public interface IVertexFormat
    {
        Vector4 Position { get; }
        RgbaFloat Color { get; }
        Vector2 TextureCoords { get; }
        int TextureId { get; }
    }
}