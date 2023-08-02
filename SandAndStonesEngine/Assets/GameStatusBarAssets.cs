using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using static System.Net.Mime.MediaTypeNames;

namespace SandAndStonesEngine.Assets
{
    public class GameStatusBarAssets : IDisposable
    {
        public RgbaFloat ClearColor;
        public IndexBuffer IndexBuffer;
        public VertexBuffer VertexBuffer;
        private bool disposedValue;

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

        public List<GameAssetBase> gameAssets = new List<GameAssetBase>();
        public GameStatusBarAssets()
        {
            this.ClearColor = RgbaFloat.Black;
        }

        private List<GameAssetBase> InitGameAssets()
        {
            QuadGridManager.Instance.InitNewBatch();
            List<GameAssetBase> assets = new List<GameAssetBase>();

            var GameAsset1 = new GameAsset(0);
            GameAsset1.Init(0, 0, 4, "status.png");
            assets.Add(GameAsset1);

            var GameFontAsset1 = new GameTextAsset(1);
            GameFontAsset1.Init(0, 0, 1, "letters.png");
            assets.Add(GameFontAsset1);
            return assets;
        }
        public void Create()
        {
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
            gameAssets.ForEach(e =>
            {
                if (e.IsText)
                {
                    string pointsText = "Points: 100000";
                    e.SetParam(pointsText);
                }
            });

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

                var disposableVertexBuffer = VertexBuffer as IDisposable;
                disposableVertexBuffer?.Dispose();
                var disposableIndexBuffer = IndexBuffer as IDisposable;
                disposableIndexBuffer?.Dispose();
                disposedValue = true;
            }
        }

        ~GameStatusBarAssets()
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
