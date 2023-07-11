using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAssets
    {
        private readonly GameGraphicDevice gameGraphicDevice;
        public RgbaFloat ClearColor;
        public IndexBuffer IndexBuffer;
        public VertexBuffer VertexBuffer;
        public GameTexture gameTexture;
        public ScreenDivisionForQuads screenDivisionForQuads;
        public Matrices matrices;
        public DeviceBuffer DeviceVertexBuffer
        {
            get { return VertexBuffer.DeviceBuffer; }
        }
        public VertexLayoutDescription[] VertexLayouts
        {
            get { return VertexBuffer.VertexLayout; }
        }

        public ResourceSet ResourceSet
        {
            get { return gameTexture.ResourceSet; }
        }

        public ResourceLayout ResourceLayout
        {
            get { return gameTexture.TextureLayout; }
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

        GameAsset GameAsset;
        InputDevicesState inputDeviceState;
        InputMotionMapperBase inputMotionMapper;
        WorldViewTransformator transformations;
        public GameAssets(GameGraphicDevice gameGraphicDevice, ScreenDivisionForQuads screenDivisionForQuads, Matrices matrices, InputDevicesState inputDeviceState)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.ClearColor = RgbaFloat.Black;
            this.screenDivisionForQuads = screenDivisionForQuads;
            this.matrices = matrices;
            this.inputDeviceState = inputDeviceState;
        }

        
        public void Create()
        {
            inputMotionMapper = new QuadInputMotionMapper(inputDeviceState);

            GameAsset = new GameAsset(screenDivisionForQuads, inputMotionMapper, matrices);
            GameAsset.Create();

            VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, GameAsset.QuadModelList);
            VertexBuffer.Create();
            VertexBuffer.Bind();

            IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, GameAsset.QuadModelList);
            IndexBuffer.Create();
            IndexBuffer.Bind();

            gameTexture = new GameTexture("wall.png", gameGraphicDevice);
            gameTexture.Init();
            gameTexture.UpdateTexture();
        }

        public void Update()
        {
            //GameAsset.Update();
        }

        public void Destroy()
        {
            gameTexture.Destroy();
            VertexBuffer.Destroy();
            IndexBuffer.Destroy();
        }
    }
}
