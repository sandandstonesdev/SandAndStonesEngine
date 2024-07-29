using SandAndStonesEngine.Buffers;
using System.Numerics;

namespace SandAndStonesEngine.DataModels.Quads
{
    public interface IQuadModel
    {
        void Init();
        VertexDataFormat[] VerticesPositions { get; }
        ushort[] QuadIndexes { get; }
        public void Move(Vector4 pixelMovementVector);
    }
}
