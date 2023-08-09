using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public abstract class GameAssetBase : IDisposable
    {
        public uint Id { get; protected set; }
        protected abstract AssetType AssetType { get; }
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
        protected RgbaFloat Color;
        private bool disposedValue;
        private object parameter;
        private bool parameterChanged = false;
        public abstract bool IsText { get; }
        public GameAssetBase(RgbaFloat color, float depth, float scale)
        {
            this.QuadModelList = new List<IQuadModel>();
            this.Color = color;
            this.Depth = depth;
            this.Scale = scale;
        }

        public abstract void Init(int startX, int startY, int endX, int endY, string textureName);

        public virtual void SetParam(object param)
        {
            this.parameter = param;
            this.parameterChanged = true;
        }

        public virtual void Update(long delta)
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
