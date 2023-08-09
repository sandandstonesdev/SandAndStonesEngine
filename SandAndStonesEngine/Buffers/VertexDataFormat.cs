using System.Numerics;
using System.Runtime.CompilerServices;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public struct VertexDataFormat
    {
        public Vector4 Position { get; set; }
        public RgbaFloat Color { get; }
        public Vector2 TextureCoords { get; }
        public int TextureId { get; }
        public uint AssetId { get; }
        public VertexDataFormat(Vector3 position, RgbaFloat color, Vector2 textureCoords, uint assetId, int textureId)
        {
            Position = new Vector4(position, 1);
            Color = color;
            TextureCoords = textureCoords;
            TextureId = textureId;
            AssetId = assetId;
        }
        //public static uint SizeInBytes { get; } = (uint)Unsafe.SizeOf<VertexDataFormat>();
        public const uint SizeInBytes = ((4 + 1 + 2) * 8) + 4 + 4;
    }
}