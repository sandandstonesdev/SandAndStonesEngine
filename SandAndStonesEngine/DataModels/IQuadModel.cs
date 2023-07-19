using SandAndStonesEngine.Buffers;

namespace SandAndStonesEngine.DataModels
{
    public interface IQuadModel
    {
        void Create();
        IVertexFormat[] VerticesPositions { get; }
        public ushort[] QuadIndexes { get; }
    }
}
