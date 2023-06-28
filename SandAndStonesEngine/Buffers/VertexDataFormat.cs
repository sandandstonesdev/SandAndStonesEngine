using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public struct VertexDataFormat
    {
        public Vector2 Position; // Normalized device coordinates.
        public RgbaFloat Color; // Vertex Color
        public Vector2 TextureCoords;
        public VertexDataFormat(Vector2 position, RgbaFloat color, Vector2 textureCoords)
        {
            Position = position;
            Color = color;
            TextureCoords = textureCoords;
        }
        public const uint SizeInBytes = (2 + 1 + 2) * 8;
    }
}