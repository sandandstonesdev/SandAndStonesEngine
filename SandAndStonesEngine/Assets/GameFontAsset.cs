using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.Assets
{
    public class GameFontAsset : IGameAsset, IDisposable
    {
        public List<IQuadModel> QuadModelList { get; private set; }
        public ITextureData GameTextureData { get; private set; }
        private float scale;
        private float depth;
        private int TextureId;
        private bool disposedValue;

        public GameFontAsset(int textureId, float depth=1, float scale= 4.0f)
        {
            this.QuadModelList = new List<IQuadModel>();
            this.TextureId = textureId;
            this.depth = depth;
            this.scale = scale;
        }

        public void Init(int start, int end, QuadGrid quadGrid, string textureName)
        {
            GameTextureData = new FontTextureData(TextureId, textureName);
            GameTextureData.Init();

            ColorRandomizer colorRandomizer = new ColorRandomizer();
            for (int i = start; i < end; i++)
            {
                for (int j = start; j < end; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, depth);
                    var color = colorRandomizer.GetColor();
                    QuadModel quadModel = new QuadModel(positionInQuadCount, scale, color, quadGrid, TextureId);
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

        ~GameFontAsset()
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
