using System.Numerics;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Utils;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAsset : IGameAsset, IDisposable
    {
        public int Id { get; private set; }
        public List<IQuadModel> QuadModelList { get; private set; }
        public ITextureData GameTextureData { get; private set; }
        private float scale;
        private float Depth;
        private int TextureId;
        private bool disposedValue;

        public GameAsset(int textureId, float depth, float scale = 1.0f)
        {
            this.QuadModelList = new List<IQuadModel>();
            this.TextureId = textureId;
            this.Depth = depth;
            this.scale = scale;
            this.Id = IdManager.GetAssetId();
        }

        public void Init(int startX, int startY, int end, QuadGrid quadGrid, string textureName)
        {
            GameTextureData = new GameTextureData(Id, TextureId, textureName);
            GameTextureData.Init();

            ColorRandomizer colorRandomizer = new ColorRandomizer();
            for (int i = startX; i < end; i++)
            {
                for (int j = startY; j < end; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    var color = colorRandomizer.GetColor();
                    IQuadModel quadModel = new QuadModel(positionInQuadCount, scale, color, quadGrid, TextureId);
                    quadModel.Create();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public void Update(double delta)
        {
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

        ~GameAsset()
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
