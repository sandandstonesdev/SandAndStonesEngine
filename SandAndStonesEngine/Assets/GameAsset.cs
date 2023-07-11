using System.Numerics;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.Utils;

namespace SandAndStonesEngine.Assets
{
    public class GameAsset
    {
        ScreenDivisionForQuads screenDivision;
        public List<QuadModel> QuadModelList = new List<QuadModel>();
        float scale = 1.0f;
        private WorldViewTransformator worldViewTransformator;
        private TransformatorData transformatorData;
        public GameAsset(ScreenDivisionForQuads screenDivision, InputMotionMapperBase inputMotionMapper, Matrices matrices)
        {
            this.screenDivision = screenDivision;
            var transformatorData = new TransformatorData(new Vector3(0, 0, 1.0f), new Vector3(0, 0, -1), new Vector3(0, 1, 0), new Vector2(0, 0), 0.002f);
            this.worldViewTransformator = new WorldViewTransformator(transformatorData, matrices, inputMotionMapper);
        }

        public void Create()
        {
            QuadGrid quadGrid = new QuadGrid(screenDivision);
            ColorRandomizer colorRandomizer = new ColorRandomizer();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var positionInQuadCount = new Vector2(i, j);
                    var color = colorRandomizer.GetColor();
                    var quadModel = new QuadModel(positionInQuadCount, scale, color, quadGrid);
                    quadModel.Create();
                    QuadModelList.Add(quadModel);
                }
            }
        }

        public void Update()
        {
            worldViewTransformator.Update();
        }
    }
}
