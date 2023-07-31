using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.Assets
{
    public abstract class GameAssetBase : IGameAsset, IDisposable
    {
        public int Id { get; private set; }
        public List<IQuadModel> QuadModelList { get; private set; }
        public ITextureData GameTextureData { get; protected set; }
        private float scale;
        private float Depth;
        protected int TextureId;
        private bool disposedValue;
        private object parameter;
        private bool parameterChanged = false;
        public abstract bool IsText { get; }
        public GameAssetBase(int textureId, float depth = 1, float scale = 4.0f)
        {
            this.QuadModelList = new List<IQuadModel>();
            this.TextureId = textureId;
            this.Depth = depth;
            this.scale = scale;
            this.Id = IdManager.GetAssetId();
        }

        public void Init(int startX, int startY, int end, QuadGrid quadGrid, string textureName)
        {
            ColorRandomizer colorRandomizer = new ColorRandomizer();
            for (int i = startX; i < end; i++)
            {
                for (int j = startY; j < end; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    var color = colorRandomizer.GetColor();
                    QuadModel quadModel = new QuadModel(positionInQuadCount, scale, color, quadGrid, TextureId);
                    quadModel.Create();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public virtual void SetParam(object param)
        {
            this.parameter = param;
            this.parameterChanged = true;
        }

        public virtual void Update(double delta)
        {
            if (this.parameterChanged)
            {
                GameTextureData.Update(this.parameter);
                this.parameterChanged = false;
            }
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
