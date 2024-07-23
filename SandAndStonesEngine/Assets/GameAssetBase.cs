using SandAndStonesEngine.Animation;
using SandAndStonesEngine.DataModels;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public abstract class GameAssetBase : IDisposable
    {
        public uint Id { get; protected set; }
        public abstract AssetType AssetType { get; }
        public abstract AssetBatchType AssetBatchType { get; init; }
        //public delegate void Animate(string param);
        //public Animate AnimateTexture { get; set; }

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
        public string Name { get; private set; }
        public abstract bool IsText { get; }
        public abstract IAnimation Animation { get; set; }
        public GameAssetBase(string name, RgbaFloat color, float depth, float scale)
        {
            this.QuadModelList = new List<IQuadModel>();
            this.Name = name;
            this.Color = color;
            this.Depth = depth;
            this.Scale = scale;
        }

        public abstract void Init(int startX, int startY, int endX, int endY, string textureName);

        public void SetAnimation(IAnimation animation)
        {
            Animation = animation;
        }

        public void Animate(string param="")
        {
            Animation.Next(param);
            this.parameterChanged = true;
        }

        public virtual void SetParam(object param)
        {
            this.parameter = param;
            this.parameterChanged = true;
        }

        public virtual void Update(long delta)
        {
            if (this.parameterChanged)
            {
                if (Animation is not null)
                    GameTextureData.Update(Animation.GetCurrent());
                else
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
