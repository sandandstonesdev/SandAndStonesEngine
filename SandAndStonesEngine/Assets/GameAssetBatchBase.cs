using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameFactories;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public abstract class GameAssetBatchBase : IDisposable
    {
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

        public List<GameAssetBase> Assets = new List<GameAssetBase>();

        protected abstract List<GameAssetBase> InitGameAssets();

        public virtual void Init()
        {
            QuadGridManager.Instance.StartNewBatch();

            var gameGraphicDevice = Factory.Instance.GetGameGraphicDevice();
            Assets = InitGameAssets();

            List<IQuadModel> quadModels = new List<IQuadModel>();
            Assets.ForEach(a => quadModels.AddRange(a.QuadModelList));

            VertexBuffer = new VertexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            VertexBuffer.Init();

            IndexBuffer = new IndexBuffer(gameGraphicDevice.GraphicsDevice, quadModels);
            IndexBuffer.Init();
        }

        public virtual void Update(long delta)
        {
            Assets.ForEach(e => e.Update(delta));
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

                Assets.ForEach(e =>
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
