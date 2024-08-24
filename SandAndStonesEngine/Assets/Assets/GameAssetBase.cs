using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using System.Numerics;

namespace SandAndStonesEngine.Assets.Assets
{
    public abstract class GameAssetBase : IDisposable
    {
        public uint Id { get; protected set; }
        public abstract AssetType AssetType { get; }
        public abstract AssetBatchType AssetBatchType { get; init; }

        public int TextureId
        {
            get
            {
                return GameTextureData.Id;
            }
        }
        public List<IQuadModel> QuadModelList { get; private set; }

        public GameTextureDataBase GameTextureData { get; protected set; }
        protected float Scale;
        protected float Depth;
        private bool disposedValue;
        public string Name { get; private set; }
        public abstract bool IsText { get; }
        public abstract IAnimation Animation { get; set; }
        public GameAssetBase(string name, float depth, float scale)
        {
            QuadModelList = [];
            Name = name;
            Depth = depth;
            Scale = scale;
        }

        public abstract GameAssetBase Init(QuadGridManager quadGridManager, AssetInfo assetInfo);

        protected virtual void CreateAssetQuad(QuadGridManager gridManager, AssetInfo assetInfo, Vector2 screenPos, Vector3 positionInQuadCount, TileType tileType)
        {
            var quadData = gridManager.GetQuadData(screenPos, positionInQuadCount, Scale, tileType);
            var quadModel = assetInfo.AssetFactory.CreateTile(gridManager, quadData, assetInfo.Textures[0].Color, Id, TextureId, tileType);
            QuadModelList.Add(quadModel);
        }

        public void Animate(string param = "", int skipFrames = 5)
        {
            Animation.Next(param, skipFrames);
        }

        public virtual void Update(long delta)
        {
            if (Animation?.Changed ?? false)
                GameTextureData.Update(Animation.GetCurrent());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                IDisposable? toDispose = GameTextureData as IDisposable;
                toDispose?.Dispose();
                disposedValue = true;
            }
        }

        ~GameAssetBase()
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
