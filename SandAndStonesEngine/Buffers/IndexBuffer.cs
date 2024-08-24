using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
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
        IQuadModel[] quadModelList;
        private ushort[] QuadIndexes;
        private bool disposedValue;
        private ScrollableViewport scrollableViewport;

        public IndexBuffer(GraphicsDevice graphicsDevice, IEnumerable<IQuadModel> quadModelList, ScrollableViewport scrollableViewport)
        {
            this.quadModelList = quadModelList.ToArray();
            this.graphicsDevice = graphicsDevice;
            this.scrollableViewport = scrollableViewport;
        }

        public void SetQuads(IEnumerable<IQuadModel> quadModels)
        {
            quadModelList = quadModels.ToArray();
        }

        private uint GetNeededBufSize(ushort[] quadIndexes)
        {
            uint bufSize = (uint)quadIndexes.Length * sizeof(ushort);
            return bufSize;
        }

        private ushort[] CollectAllIndicesFromQuads()
        {
            var quadModelCount = quadModelList.Count();
            var indicesDataLength = quadModelCount * 6;
            ushort[] indexData = new ushort[indicesDataLength];
            int destinationIdx = 0;
            for (int i = 0; i < quadModelCount; i++)
            {
                if (((IVisibleModel)quadModelList[i]).IsVisible(scrollableViewport))
                {
                    for (int j = 0; j < 6; j++)
                    {
                        indexData[destinationIdx++] = quadModelList[i].QuadIndexes[j];
                    }
                }
            }

            return indexData;
            //List<ushort> indexData = new List<ushort>();
            //foreach (var quadModel in quadModelList)
            //{
            //    indexData.AddRange(quadModel.QuadIndexes);
            //}
            //return indexData.ToArray();
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
