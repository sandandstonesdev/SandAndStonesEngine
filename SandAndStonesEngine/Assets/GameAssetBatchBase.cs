using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.Managers;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public abstract class GameAssetBatchBase : IDisposable
    {
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

        protected abstract void InitGameAssets();

        public virtual void Init()
        {
            QuadGridManager.Instance.StartNewBatch();

            var gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
            InitGameAssets();

            if (AssetBatchType == AssetBatchType.ClientRectBatch)
            {
                VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, AssetDataManager.Instance.ModelData);
                VertexBuffer.Init();

                IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, AssetDataManager.Instance.ModelData);
                IndexBuffer.Init();
            }
            else if (AssetBatchType == AssetBatchType.StatusBarBatch)
            {
                VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, AssetDataManager.Instance.StatusBarModels);
                VertexBuffer.Init();

                IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, AssetDataManager.Instance.StatusBarModels);
                IndexBuffer.Init();
            }
        }

        public virtual void Update(long delta)
        {
            AssetDataManager.Instance.Assets.ForEach(e => e.Update(delta));
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
