using System.Numerics;
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
        public Vector2 Scroll { get; private set; }
        public Vector2 Movement { get; private set; }

        public VertexDataFormat(Vector3 position, RgbaFloat color, Vector2 textureCoords, uint assetId, int textureId)
        {
            Position = new Vector4(position, 1);
            Color = color;
            TextureCoords = textureCoords;
            TextureId = textureId;
            AssetId = assetId;
            Scroll = Vector2.Zero;
        }

        public void SetScroll(Vector2 scroll)
        {
            Scroll = scroll;
        }

        public void SetMovement(Vector2 movement)
        {
            Movement = movement;
        }

        //public static uint SizeInBytes { get; } = (uint)Unsafe.SizeOf<VertexDataFormat>();
        public const uint SizeInBytes = ((4 + 1 + 2) * 8) + 4 + 4 + 8;
    }
}