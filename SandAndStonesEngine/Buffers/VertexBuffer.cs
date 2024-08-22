using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public class VertexBuffer : IDisposable
    {
        public DeviceBuffer DeviceBuffer;
        readonly GraphicsDevice graphicsDevice;
        public static VertexLayoutDescription Layout
        {
            get => VertexLayout.VertexLayoutPrototype;
        }

        private IQuadModel[] quadModelList;
        private ScrollableViewport scrollableViewport;

        public VertexBuffer(GraphicsDevice graphicDevice, IEnumerable<IQuadModel> quadModelList, ScrollableViewport scrollableViewport)
        {
            this.graphicsDevice = graphicDevice;
            this.quadModelList = quadModelList.ToArray();
            this.scrollableViewport = scrollableViewport;
        }

        VertexDataFormat[] VertexData;
        private bool disposedValue;

        public void SetQuads(IEnumerable<IQuadModel> quadModels)
        {
            quadModelList = quadModels.ToArray();
        }

        private VertexDataFormat[] CollectAllVerticesFromQuads()
        {
            var quadModelCount = quadModelList.Count();
            var verticesDataLength = quadModelCount * 4;
            VertexDataFormat[] quadsVerticesData = new VertexDataFormat[verticesDataLength];
            int destinationIdx = 0;

            for (int i = 0; i < quadModelCount; i++)
            {
                if (((IVisibleModel)quadModelList[i]).IsVisible(scrollableViewport))
                {
                    for (int j = 0; j < 4; j++)
                    {
                        quadsVerticesData[destinationIdx++] = quadModelList[i].VerticesPositions[j];
                    }
                }
            }

            return quadsVerticesData;
            //VertexDataFormat> quadsVerticesData = new List<VertexDataFormat>();
            //foreach (var quadModel in quadModelList)
            //{
            //    quadsVerticesData.AddRange(quadModel.VerticesPositions);
            //}
        }

        private uint GetNeededBufSize(VertexDataFormat[] vertexData)
        {
            uint bufSize = (uint)vertexData.Length * VertexDataFormat.SizeInBytes;
            return bufSize;
        }

        public void Init()
        {
            ResourceFactory factory = graphicsDevice.ResourceFactory;
            VertexData = CollectAllVerticesFromQuads();
            DeviceBuffer = factory.CreateBuffer(new BufferDescription(GetNeededBufSize(VertexData), BufferUsage.VertexBuffer));
        }

        public void Update()
        {
            VertexData = CollectAllVerticesFromQuads();
            graphicsDevice.UpdateBuffer(DeviceBuffer, 0, VertexData);
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

        ~VertexBuffer()
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
