using SandAndStonesEngine.Animation;
using SandAndStonesEngine.Assets.Textures;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesLibrary.AssetConfig;
using System.Numerics;

namespace SandAndStonesEngine.Assets.Assets
{
    public abstract class GameAssetBase : IDisposable
    {
        public uint Id { get; protected set; }
        public abstract AssetType AssetType { get; }
        public abstract AssetBatchType AssetBatchType { get; init; }
        public List<IQuadModel> QuadModelList { get; private set; }

        public TextureInfo TextureInfo;
        protected float Scale;
        protected float Depth;
        private bool disposedValue;
        public string Name { get; private set; }
        public abstract bool IsText { get; }
        protected IAnimation Animation { get; set; }
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
            var quadModel = assetInfo.AssetFactory.CreateTile(gridManager, quadData, assetInfo.Textures[0].Color, Id, assetInfo.Textures[0].Id, tileType);
            QuadModelList.Add(quadModel);
        }

        public void Animate(string param = "", int skipFrames = 5)
        {
            Animation.Next(param, skipFrames);
        }

        public virtual void Update(long delta)
        {
            if (Animation?.Changed ?? false)
                TextureInfo.Update(Animation.GetCurrent());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                TextureInfo?.Dispose();
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
