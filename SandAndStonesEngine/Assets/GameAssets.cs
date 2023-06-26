using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.Utils;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    internal class GameAssets
    {
        private readonly GameGraphicDevice gameGraphicDevice;
        public RgbaFloat ClearColor;
        public IndexBuffer IndexBuffer;
        public VertexBuffer VertexBuffer;
        public ScreenDivisionForQuads screenDivisionForQuads;

        public DeviceBuffer DeviceVertexBuffer
        {
            get { return VertexBuffer.DeviceBuffer; }
        }
        public VertexLayoutDescription[] VertexLayouts
        {
            get { return VertexBuffer.VertexLayout; }
        }
        public uint IndicesCount
        {
            get { return IndexBuffer.IndicesCount; }
        }
        public DeviceBuffer DeviceIndexBuffer
        {
            get { return IndexBuffer.DeviceBuffer; }
        }

        public IndexFormat IndexBufferFormat
        {
            get { return IndexBuffer.IndexBufferFormat; }
        }

        public GameAssets(GameGraphicDevice gameGraphicDevice, ScreenDivisionForQuads screenDivisionForQuads)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.ClearColor = RgbaFloat.Black;
            this.screenDivisionForQuads = screenDivisionForQuads;
        }

        
        public void Create()
        {
            QuadGrid quadGrid = new QuadGrid(screenDivisionForQuads);
            ColorRandomizer colorRandomizer = new ColorRandomizer();
            List<QuadModel> quadModelList = new List<QuadModel>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    var positionInQuadCount = new Vector2(i, j);
                    var color = colorRandomizer.GetColor();
                    var quadModel = new QuadModel(positionInQuadCount, color, quadGrid);
                    quadModel.Init();
                    quadModelList.Add(quadModel);
                }
            }

            VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, quadModelList);
            VertexBuffer.Create();
            VertexBuffer.Bind();

            IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, quadModelList);
            IndexBuffer.Create();
            IndexBuffer.Bind();
        }

        public void Destroy()
        {
            VertexBuffer.Destroy();
            IndexBuffer.Destroy();
        }
    }
}
