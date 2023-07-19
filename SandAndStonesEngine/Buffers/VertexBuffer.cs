﻿using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Utils;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.Buffers
{
    public class VertexBuffer
    {
        public DeviceBuffer DeviceBuffer;
        readonly GraphicsDevice graphicsDevice;
        public VertexLayoutDescription VertexLayout
        {
            get 
            {
                var VertexLayout = new VertexLayout();
                return VertexLayout.Layout;
            }
        }

        List<QuadModel> quadModelList;
        public VertexBuffer(GraphicsDevice graphicDevice, List<QuadModel> quadModelList)
        {
            this.graphicsDevice = graphicDevice;
            this.quadModelList = quadModelList;
        }

        VertexDataFormat[] VertexData;

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

        public void Create()
        {
            ResourceFactory factory = graphicsDevice.ResourceFactory;
            VertexData = CollectAllVerticesFromQuads();
            DeviceBuffer = factory.CreateBuffer(new BufferDescription(GetNeededBufSize(VertexData), BufferUsage.VertexBuffer));
        }

        public void Update()
        {
            //int i = 0;
            //VertexDataFormat[] vertexData = new VertexDataFormat[VertexData.Length];
            //foreach (var v in VertexData)
            //{
            //    vertexData[i] = (VertexDataFormat)v;
            //}
            VertexData = CollectAllVerticesFromQuads();
            graphicsDevice.UpdateBuffer(DeviceBuffer, 0, VertexData);
        }

        public void Destroy()
        {
            DeviceBuffer.Dispose();
        }
    }
}
