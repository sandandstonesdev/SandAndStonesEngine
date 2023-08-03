using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Utils;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public class VertexBuffer : IDisposable
    {
        public DeviceBuffer DeviceBuffer;
        readonly GraphicsDevice graphicsDevice;
        public static VertexLayoutDescription VertexLayout
        {
            get => VertexInstanceLayout.VertexLayoutPrototype;
        }

        List<IQuadModel> quadModelList;
        public VertexBuffer(GraphicsDevice graphicDevice, List<IQuadModel> quadModelList)
        {
            this.graphicsDevice = graphicDevice;
            this.quadModelList = quadModelList;
        }

        VertexDataFormat[] VertexData;
        private bool disposedValue;

        private VertexDataFormat[] CollectAllVerticesFromQuads()
        {
            List<VertexDataFormat> quadsVerticesData = new List<VertexDataFormat>();
            foreach (var quadModel in quadModelList)
            {
                quadsVerticesData.AddRange(quadModel.VerticesPositions);
            }
            return quadsVerticesData.ToArray();
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
