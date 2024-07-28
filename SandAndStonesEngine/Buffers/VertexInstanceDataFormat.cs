using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public struct VertexInstanceDataFormat : IVertexFormat
    {
        public Vector4 Position { get; }
        public RgbaFloat Color { get; }
        public Vector2 TextureCoords { get; }
        public int TextureId { get; }
        public int Scroll { get; }
        public VertexInstanceDataFormat(Vector3 position, RgbaFloat color, Vector2 textureCoords, int textureId, int scroll=0)
        {
            Position = new Vector4(position, 1);
            Color = color;
            TextureCoords = textureCoords;
            TextureId = textureId;
            Scroll = scroll;
        }

        public const uint SizeInBytes = (4 + 1 + 2) * 8;
        //public static uint SizeInBytes { get; } = (uint)Unsafe.SizeOf<VertexInstanceDataFormat>();
    }
}
