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
    public class GameFontAsset
    {

        ScreenDivisionForQuads screenDivision;
        public List<QuadModel> QuadModelList = new List<QuadModel>();
        float scale = 4.0f;
        public float Depth;
        public int TextureId;
        public FontTextureData GameTextureData;
        public AutoPinner PinnedImageBytes;
        public int BytesCount;

        public GameFontAsset(int textureId, ScreenDivisionForQuads screenDivision, float depth)
        {
            this.screenDivision = screenDivision;
            this.TextureId = textureId;
            this.Depth = 1;
        }

        public void Create(int start, int end, QuadGrid quadGrid, string textureName)
        {
            GameTextureData = new FontTextureData(TextureId, textureName);
            GameTextureData.Init();

            ColorRandomizer colorRandomizer = new ColorRandomizer();
            for (int i = start; i < end; i++)
            {
                for (int j = start; j < end; j++)
                {
                    var positionInQuadCount = new Vector3(i, j, Depth);
                    var color = colorRandomizer.GetColor();
                    QuadModel quadModel = new QuadModel(positionInQuadCount, scale, color, quadGrid, TextureId);
                    quadModel.Create();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public void Update(double delta)
        {
            //foreach (var quadModel in QuadModelList)
            //{
            //    //quadModel.Move(movementVector);
            //}
            ////Thread.Sleep(5);
        }

        public void Destroy()
        {
            GameTextureData.Destroy();
        }
    }
}
