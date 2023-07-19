using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameFactories;
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
        public ScreenDivisionForQuads screenDivisionForQuads;
        public DeviceBuffer DeviceVertexBuffer
        {
            get { return VertexBuffer.DeviceBuffer; }
        }
        public VertexLayoutDescription[] VertexLayouts
        {
            get { return new VertexLayoutDescription[] { VertexBuffer.VertexLayout }; }
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

        GameAsset GameAsset1;
        GameAsset GameAsset2;
        GameAsset BackgroundAsset;
        InputDevicesState inputDeviceState;
        InputMotionMapperBase inputMotionMapper;
        public Matrices Matrices;
        public GameTextureSurface gameTexture;

        public ResourceSet ResourceSet
        {
            get { return gameTexture.ResourceSet; }
        }

        public ResourceLayout ResourceLayout
        {
            get { return gameTexture.TextureLayout; }
        }

        public TransformatorData transformatorData;

        public GameAssets(ScreenDivisionForQuads screenDivisionForQuads, Matrices matrices, InputDevicesState inputDeviceState, TransformatorData transformatorData)
        {
            this.ClearColor = RgbaFloat.Black;
            this.transformatorData = transformatorData;
            this.screenDivisionForQuads = screenDivisionForQuads;
            this.Matrices = matrices;
            this.inputDeviceState = inputDeviceState;
        }

        WorldTransformator worldTransformator;
        public void Create()
        {
            var gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();

            inputMotionMapper = new QuadInputMotionMapper(inputDeviceState);

            QuadGrid quadGrid = new QuadGrid(screenDivisionForQuads);
            worldTransformator = new WorldTransformator(Matrices, inputMotionMapper, transformatorData);

            int assetTextureId = 0;
            BackgroundAsset = new GameAsset(assetTextureId, screenDivisionForQuads, -1);
            BackgroundAsset.Create(0, 4, quadGrid, "wall.png");

            assetTextureId++;
            GameAsset1 = new GameAsset(assetTextureId, screenDivisionForQuads, 1);
            GameAsset1.Create(0, 1, quadGrid, "char1.png");

            assetTextureId++;
            GameAsset2 = new GameAsset(assetTextureId, screenDivisionForQuads, 1);
            GameAsset2.Create(1, 2, quadGrid, "char2.png");

            List<QuadModel> quadModels = new List<QuadModel>();
            quadModels.AddRange(BackgroundAsset.QuadModelList);
            quadModels.AddRange(GameAsset1.QuadModelList);
            quadModels.AddRange(GameAsset2.QuadModelList);

            VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            VertexBuffer.Create();
            VertexBuffer.Update();

            IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            IndexBuffer.Create();
            IndexBuffer.Update();

            var Tex1Data = BackgroundAsset.GameTextureData;
            var Tex2Data = GameAsset1.GameTextureData;
            var Tex3Data = GameAsset2.GameTextureData;
            List<GameTextureData> textureDataList = new List<GameTextureData>();
            textureDataList.Add(Tex1Data);
            textureDataList.Add(Tex2Data);
            textureDataList.Add(Tex3Data);

            gameTexture = new GameTextureSurface(textureDataList, 256, 256);
            gameTexture.Init();
            gameTexture.UpdateTextureArray();
        }

        public void Update(double delta)
        {
            worldTransformator.Update();
            GameAsset1.Update(delta);
            GameAsset2.Update(delta);
            VertexBuffer.Update();
        }

        public void Destroy()
        {
            VertexBuffer.Destroy();
            IndexBuffer.Destroy();
        }
    }
}
