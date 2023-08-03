using SandAndStonesEngine.Buffers;

namespace SandAndStonesEngine.DataModels
{
    public interface IQuadModel
    {
        void Init();
        VertexDataFormat[] VerticesPositions { get; }
        public ushort[] QuadIndexes { get; }
    }
}
