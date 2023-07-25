using SandAndStonesEngine.Buffers;

namespace SandAndStonesEngine.DataModels
{
    public interface IQuadModel
    {
        void Create();
        VertexDataFormat[] VerticesPositions { get; }
        public ushort[] QuadIndexes { get; }
    }
}
