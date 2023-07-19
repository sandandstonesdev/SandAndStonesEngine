using System.Numerics;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Utils;
using Veldrid;

namespace SandAndStonesEngine.Assets
{
    public class GameAsset
    {
        ScreenDivisionForQuads screenDivision;
        public List<QuadModel> QuadModelList = new List<QuadModel>();
        float scale = 1.0f;
        public float Depth;
        public int TextureId;
        public GameTextureData GameTextureData;
        
        public GameAsset(int textureId, ScreenDivisionForQuads screenDivision, float depth)
        {
            this.screenDivision = screenDivision;
            this.TextureId = textureId;
            this.Depth = depth;
        }

        public void Create(int start, int end, QuadGrid quadGrid, string textureName)
        {
            GameTextureData = new GameTextureData(TextureId, textureName);
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
