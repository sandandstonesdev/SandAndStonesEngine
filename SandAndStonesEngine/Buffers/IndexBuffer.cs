using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Utils;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public class IndexBuffer
    {
        public IndexFormat IndexBufferFormat = IndexFormat.UInt16;
        public DeviceBuffer DeviceBuffer;
        readonly GraphicsDevice graphicsDevice;
        public uint IndicesCount
        {
            get { return (uint)QuadIndexes.Length; }
        }
        List<QuadModel> quadModelList;
        ushort[] QuadIndexes;
        public IndexBuffer(GraphicsDevice graphicsDevice, List<QuadModel> quadModelList)
        {
            this.quadModelList = quadModelList;
            this.graphicsDevice = graphicsDevice;
        }

        private uint GetNeededBufSize(ushort[] quadIndexes)
        {
            uint bufSize = (uint)quadIndexes.Length * sizeof (ushort);
            return bufSize;
        }

        private ushort[] CollectAllIndicesFromQuads()
        {
            List<ushort> indexData = new List<ushort>();
            foreach (var quadModel in quadModelList)
            {
                indexData.AddRange(quadModel.QuadIndexes);
            }
            return indexData.ToArray();
        }

        public void Create()
        {
            QuadIndexes = CollectAllIndicesFromQuads();
            ResourceFactory factory = graphicsDevice.ResourceFactory;
            DeviceBuffer = factory.CreateBuffer(new BufferDescription(GetNeededBufSize(QuadIndexes), BufferUsage.IndexBuffer));
        }

        public void Bind()
        {
            graphicsDevice.UpdateBuffer(DeviceBuffer, 0, QuadIndexes);
        }

        public void Destroy()
        {
            DeviceBuffer.Dispose();
        }
    }
}
