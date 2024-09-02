using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels.ScreenDivisions;
using System.Numerics;

namespace SandAndStonesEngine.DataModels.Quads
{
    public interface IQuadModel
    {
        void Init(ScreenQuadCalculator screenDivisions);
        VertexDataFormat[] VerticesPositions { get; }
        ushort[] QuadIndexes { get; }
        public void Move(Vector4 pixelMovementVector);
    }
}
