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
    public class GameAssets : IDisposable
    {
        public RgbaFloat ClearColor;
        public IndexBuffer IndexBuffer;
        public VertexBuffer VertexBuffer;
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

        private bool disposedValue;

        public List<GameAssetBase> gameAssets = new List<GameAssetBase>();
        FPSCalculator fpsCalculator;
        public GameAssets()
        {
            this.fpsCalculator = new FPSCalculator(10);
            this.ClearColor = RgbaFloat.Black;
        }

        private List<GameAssetBase> InitGameAssets()
        {
            int assetId = 0;
            List<GameAssetBase> assets = new List<GameAssetBase>();

            var BackgroundAsset = new GameAsset(-1);
            BackgroundAsset.Init(0, 0, 4, "wall.png");
            assets.Add(BackgroundAsset);

            var GameAsset1 = new GameAsset(1, 0.5f);
            GameAsset1.Init(0, 0, 1, "char1.png");
            assets.Add(GameAsset1);

            var GameAsset2 = new GameAsset(0.5f);
            GameAsset2.Init(1, 1, 2, "char2.png");
            assets.Add(GameAsset2);

            var GameFontAsset1 = new GameTextAsset(1);
            GameFontAsset1.Init(0, 0, 1, "letters.png");
            assets.Add(GameFontAsset1);
            return assets;
        }
        public void Create()
        {
            QuadGridManager.Instance.StartNewBatch();

            var gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
            gameAssets = InitGameAssets();

            List<IQuadModel> quadModels = new List<IQuadModel>();
            gameAssets.ForEach(a => quadModels.AddRange(a.QuadModelList));
            
            VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            VertexBuffer.Init();

            IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            IndexBuffer.Init();
        }

        public void Update(double delta)
        {
            fpsCalculator.AddNextDelta(delta);

            if (fpsCalculator.CanDoUpdate(delta))
            {
                int fps = (int)fpsCalculator.GetResult();
                string text = $"FPS: {fps}";
                
                gameAssets.ForEach(e =>
                {
                    if (e.IsText)
                    {
                        e.SetParam(text);
                    }
                });
            }

            gameAssets.ForEach(e => e.Update(delta));
            IndexBuffer.Update();
            VertexBuffer.Update();
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
