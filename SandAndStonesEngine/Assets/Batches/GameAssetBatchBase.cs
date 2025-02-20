using SandAndStonesEngine.Buffers;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MemoryStores;
using SandAndStonesLibrary.Assets;
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
            get { return [VertexBuffer.Layout]; }
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

        protected readonly ScrollableViewport scrollableViewport;

        private readonly GameGraphicDevice graphicDevice;
        protected readonly AssetMemoryStore assetMemoryStore;
        protected readonly AssetFactory assetFactory;

        protected GameAssetBatchBase(AssetFactory assetFactory, AssetMemoryStore assetMemoryStore, GameGraphicDevice graphicDevice, ScrollableViewport scrollableViewport)
        {
            this.graphicDevice = graphicDevice;
            this.scrollableViewport = scrollableViewport;
            this.assetMemoryStore = assetMemoryStore;
            this.assetFactory = assetFactory;
        }

        protected virtual void InitGameAssets()
        {
            VertexBuffer = new VertexBuffer(graphicDevice.GraphicsDevice, Assets, scrollableViewport);
            VertexBuffer.Init();

            IndexBuffer = new IndexBuffer(graphicDevice.GraphicsDevice, Assets, scrollableViewport);
            IndexBuffer.Init();
        }

        public virtual void Init(GameGraphicDevice device, ScrollableViewport scrollableViewport)
        {
            InitGameAssets();
        }

        public virtual void Update(long delta)
        {
            assetMemoryStore.Assets.ForEach(e =>
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

                assetMemoryStore.Assets.ForEach(e =>
                {
                    e?.Dispose();
                });

                VertexBuffer?.Dispose();
                IndexBuffer?.Dispose();
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
