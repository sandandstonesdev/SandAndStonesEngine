﻿using System;
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
    public class GameAssets : IDisposable
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

        InputDevicesState inputDeviceState;
        InputMotionMapperBase inputMotionMapper;
        public Matrices Matrices;

        public ResourceSet ResourceSet
        {
            get { return gameTextureSurface.ResourceSet; }
        }

        public ResourceLayout ResourceLayout
        {
            get { return gameTextureSurface.TextureLayout; }
        }

        public TransformatorData transformatorData;
        private WorldTransformator worldTransformator;
        private bool disposedValue;

        public List<IGameAsset> gameAssets = new List<IGameAsset>();
        GameTextureSurface gameTextureSurface;
        FPSCalculator fpsCalculator;
        public GameAssets(GameTextureSurface gameTextureSurface, ScreenDivisionForQuads screenDivisionForQuads, Matrices matrices, InputDevicesState inputDeviceState, TransformatorData transformatorData)
        {
            this.fpsCalculator = new FPSCalculator(10);
            this.ClearColor = RgbaFloat.Black;
            this.transformatorData = transformatorData;
            this.screenDivisionForQuads = screenDivisionForQuads;
            this.Matrices = matrices;
            this.inputDeviceState = inputDeviceState;
            this.gameTextureSurface = gameTextureSurface;
        }

        private List<IGameAsset> InitGameAssets(QuadGrid quadGrid)
        {
            int assetId = 0;
            List<IGameAsset> assets = new List<IGameAsset>();

            var BackgroundAsset = new GameAsset(assetId++, -1);
            BackgroundAsset.Init(0, 0, 4, quadGrid, "wall.png");
            assets.Add(BackgroundAsset);

            var GameAsset1 = new GameAsset(assetId++, 1, 0.5f);
            GameAsset1.Init(0, 0, 1, quadGrid, "char1.png");
            assets.Add(GameAsset1);

            var GameAsset2 = new GameAsset(assetId++, 0.5f);
            GameAsset2.Init(1, 1, 2, quadGrid, "char2.png");
            assets.Add(GameAsset2);

            var GameFontAsset1 = new GameFontAsset(assetId++, 1);
            GameFontAsset1.Init(0, 0, 1, quadGrid, "letters.png");
            assets.Add(GameFontAsset1);
            return assets;
        }
        public void Create()
        {
            var gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();

            inputMotionMapper = new QuadInputMotionMapper(inputDeviceState);

            QuadGrid quadGrid = new QuadGrid(screenDivisionForQuads);
            worldTransformator = new WorldTransformator(Matrices, inputMotionMapper, transformatorData);

            gameAssets = InitGameAssets(quadGrid);

            List<IQuadModel> quadModels = new List<IQuadModel>();
            gameAssets.ForEach(a => quadModels.AddRange(a.QuadModelList));
            
            VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            VertexBuffer.Init();

            IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            IndexBuffer.Create();

            gameAssets.ForEach(a => gameTextureSurface.AddToTextureDataList(a.GameTextureData));
        }

        public void Update(double delta)
        {
            fpsCalculator.AddNextDelta(delta);

            if (fpsCalculator.CanDoUpdate(delta))
            {
                int fps = (int)fpsCalculator.GetResult();
                string text = $"FPS: {fps}";
                var textAssets = gameAssets.Select(e =>
                {
                    var textAsset = e as ITextAsset;
                    textAsset?.SetText(text);
                    return e;
                }).ToList();
            }

            worldTransformator.Update();
            gameAssets.ForEach(e => e.Update(delta));
            IndexBuffer.Update();
            VertexBuffer.Update();
            gameTextureSurface.UpdateTextureArray(0, 3);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                gameAssets.ForEach(e =>
                {
                    var disposableAssets = e as IDisposable;
                    disposableAssets?.Dispose();
                });

                var disposableTextureSurface = gameTextureSurface as IDisposable;
                disposableTextureSurface?.Dispose();
                var disposableVertexBuffer = VertexBuffer as IDisposable;
                disposableVertexBuffer?.Dispose();
                var disposableIndexBuffer = IndexBuffer as IDisposable;
                disposableIndexBuffer?.Dispose();
                disposedValue = true;
            }
        }

        ~GameAssets()
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
