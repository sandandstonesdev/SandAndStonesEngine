using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public struct VertexDataFormat
    {
        public Vector2 Position; // Normalized device coordinates.
        public RgbaFloat Color; // Vertex Color
        public VertexDataFormat(Vector2 position, RgbaFloat color)
        {
            Position = position;
            Color = color;
        }
        public const uint SizeInBytes = 24;
    }
}