using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.Assets
{
    public class GameFontAsset : ITextAsset, IGameAsset, IDisposable
    {
        public int Id { get; }
        public List<IQuadModel> QuadModelList { get; private set; }
        public ITextureData GameTextureData { get; private set; }
        private float scale;
        private float depth;
        private int TextureId;
        private bool disposedValue;
        readonly string newLineChar = System.Environment.NewLine;
        string text = string.Empty;
        FPSCalculator fpsCalculator;
        public GameFontAsset(int textureId, float depth=1, float scale= 4.0f)
        {
            this.fpsCalculator = new FPSCalculator(10);
            this.QuadModelList = new List<IQuadModel>();
            this.TextureId = textureId;
            this.depth = depth;
            this.scale = scale;
        }

        public void Init(int startX, int startY, int end, QuadGrid quadGrid, string textureName)
        {
            GameTextureData = new FontTextureData(Id, TextureId);
            GameTextureData.Init();

            ColorRandomizer colorRandomizer = new ColorRandomizer();
            for (int i = startX; i < end; i++)
            {
                for (int j = startY; j < end; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, depth);
                    var color = colorRandomizer.GetColor();
                    QuadModel quadModel = new QuadModel(positionInQuadCount, scale, color, quadGrid, TextureId);
                    quadModel.Create();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public void SetText(string text)
        {
            this.text = text;
        }

        public void Update(double delta)
        {
            GameTextureData.Update(this.text);
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
