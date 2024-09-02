using SandAndStonesEngine.DataModels.Quads;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public class InstanceVertexBuffer
    {
        public DeviceBuffer DeviceBuffer;
        readonly GraphicsDevice graphicsDevice;
        public static VertexLayoutDescription Layout
        {
            get => VertexInstanceLayout.VertexLayoutPrototype;
        }

        List<IQuadModel> quadModelList;
        public InstanceVertexBuffer(GraphicsDevice graphicDevice, List<IQuadModel> quadModelList)
        {
            this.graphicsDevice = graphicDevice;
            this.quadModelList = quadModelList;
        }

        IVertexFormat[] VertexData = Array.Empty<IVertexFormat>();

        private IVertexFormat[] CollectAllVerticesFromQuads()
        {
            List<IVertexFormat> quadsVerticesData = new List<IVertexFormat>();
            foreach (var quadModel in quadModelList)
            {
                //quadsVerticesData.AddRange(quadModel.VerticesPositions);
            }
            return quadsVerticesData.ToArray();
        }

        private uint GetNeededBufSize(IVertexFormat[] vertexData)
        {
            uint bufSize = (uint)vertexData.Length * VertexInstanceDataFormat.SizeInBytes;
            return bufSize;
        }
        public void Init()
        {
            ResourceFactory factory = graphicsDevice.ResourceFactory;
            VertexData = CollectAllVerticesFromQuads();
            DeviceBuffer = factory.CreateBuffer(new BufferDescription(GetNeededBufSize(VertexData), BufferUsage.VertexBuffer));
        }

        public void Bind()
        {
            int i = 0;
            VertexInstanceDataFormat[] vertexData = new VertexInstanceDataFormat[VertexData.Length];
            foreach (var v in VertexData)
            {
                vertexData[i] = (VertexInstanceDataFormat)v;
            }
            graphicsDevice.UpdateBuffer(DeviceBuffer, 0, vertexData);
        }

        public void Destroy()
        {
            DeviceBuffer.Dispose();
        }
    }
}
