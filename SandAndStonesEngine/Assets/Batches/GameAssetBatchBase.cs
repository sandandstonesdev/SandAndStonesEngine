using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.Managers;
using Veldrid;

namespace SandAndStonesEngine.Assets.Batches
{
    public abstract class GameAssetBatchBase : IDisposable
    {
        public abstract List<IQuadModel> Assets { get; }

        public abstract AssetBatchType AssetBatchType { get; }
        public IndexBuffer IndexBuffer;
        public VertexBuffer VertexBuffer;
        public DeviceBuffer DeviceVertexBuffer
        {
            get { return VertexBuffer.DeviceBuffer; }
        }
        public VertexLayoutDescription[] VertexLayouts
        {
            get { return new VertexLayoutDescription[] { VertexBuffer.Layout }; }
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

        protected ScrollableViewport scrollableViewport;

        protected virtual void InitGameAssets()
        {
            var gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();

            VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, Assets);
            VertexBuffer.Init();

            IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, Assets);
            IndexBuffer.Init();
        }

        public GameAssetBatchBase(ScrollableViewport scrollableViewport)
        {
            this.scrollableViewport = scrollableViewport;
        }

        public virtual void Init(ScrollableViewport scrollableViewport)
        {
            InitGameAssets();
        }

        public virtual void Update(long delta)
        {
            AssetDataManager.Instance.Assets.ForEach(e =>
            {
                if (e.AssetBatchType == AssetBatchType)
                    e.Update(delta);
            });

            IndexBuffer.SetQuads(Assets);
            IndexBuffer.Update();
            VertexBuffer.SetQuads(Assets);
            VertexBuffer.Update();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                AssetDataManager.Instance.Assets.ForEach(e =>
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

        ~GameAssetBatchBase()
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
