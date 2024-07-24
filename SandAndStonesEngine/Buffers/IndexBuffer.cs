using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Utils;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public class IndexBuffer : IDisposable
    {
        public IndexFormat IndexBufferFormat = IndexFormat.UInt16;
        public DeviceBuffer DeviceBuffer;
        readonly GraphicsDevice graphicsDevice;
        public uint IndicesCount
        {
            get { return (uint)QuadIndexes.Length; }
        }
        IEnumerable<IQuadModel> quadModelList;
        ushort[] QuadIndexes;
        private bool disposedValue;

        public IndexBuffer(GraphicsDevice graphicsDevice, IEnumerable<IQuadModel> quadModelList)
        {
            this.quadModelList = quadModelList;
            this.graphicsDevice = graphicsDevice;
        }

        public void SetQuads(IEnumerable<IQuadModel> quadModels)
        {
            quadModelList = quadModels;
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

        public void Init()
        {
            QuadIndexes = CollectAllIndicesFromQuads();
            ResourceFactory factory = graphicsDevice.ResourceFactory;
            DeviceBuffer = factory.CreateBuffer(new BufferDescription(GetNeededBufSize(QuadIndexes), BufferUsage.IndexBuffer));
        }

        public void Update()
        {
            QuadIndexes = CollectAllIndicesFromQuads();
            graphicsDevice.UpdateBuffer(DeviceBuffer, 0, QuadIndexes);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                DeviceBuffer.Dispose();

                disposedValue = true;
            }
        }
        ~IndexBuffer()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
