using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public struct VertexDataFormat
    {
        public Vector4 Position;
        public RgbaFloat Color; // Vertex Color
        public Vector2 TextureCoords;
        public VertexDataFormat(Vector3 position, RgbaFloat color, Vector2 textureCoords)
        {
            Position = new Vector4(position, 1);
            Color = color;
            TextureCoords = textureCoords;
        }
        public const uint SizeInBytes = (4 + 1 + 2) * 8;
    }
}