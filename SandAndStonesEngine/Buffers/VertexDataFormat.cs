using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public struct VertexDataFormat
    {
        public Vector3 Position; // Normalized device coordinates.
        public RgbaFloat Color; // Vertex Color
        public Vector2 TextureCoords;
        public VertexDataFormat(Vector3 position, RgbaFloat color, Vector2 textureCoords)
        {
            Position = position;
            Color = color;
            TextureCoords = textureCoords;
        }
        public const uint SizeInBytes = (3 + 1 + 2) * 8;
    }
}