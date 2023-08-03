using SandAndStonesEngine.Assets;
using SandAndStonesEngine.Utils;
using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class QuadGridManager
    {
        private static readonly Lazy<QuadGridManager> lazyInstance = new Lazy<QuadGridManager>(() => new QuadGridManager());
        public static QuadGridManager Instance => lazyInstance.Value;

        private Vector3 relativePosition = new Vector3(-1.00f, 1.00f, 0.0f);
        Vector2[] PointGrid;
        int pointColumns;
        int pointRows;
        ScreenDivisionForQuads screenDivision;
        int quadId = 0;
        int quadBatchId = 0;
        public QuadGridManager()
        {
            
        }

        public void Init(ScreenDivisionForQuads screenDivision)
        {
            this.screenDivision = screenDivision;
            this.pointColumns = screenDivision.QuadCountX + 1;
            this.pointRows = screenDivision.QuadCountY + 1;
            IVertexGenerator vertexGenerator = new VertexGenerator(pointRows, pointColumns);
            vertexGenerator.Generate();
            this.PointGrid = vertexGenerator.Points;
            this.quadId = 0;
        }

        public void StartNewBatch()
        {
            quadBatchId = IdManager.GetNextQuadBatchId();
            quadId = IdManager.GetNextQuadId();
        }
        public void Resize(int screenWidth, int screenHeight)
        {
            screenDivision.Resize(screenWidth, screenHeight);
        }

        public QuadData GetQuadData(int xQuad, int yQuad, int zQuad)
        {
            Vector3[] quadPoints = new Vector3[4];
            int upperPointGridIndex = (yQuad + xQuad * pointColumns);
            int lowerPointGridIndex = (yQuad + (xQuad + 1) * pointRows);
            quadPoints[0] = new Vector3(PointGrid[upperPointGridIndex], (float)zQuad); // Upper Left
            quadPoints[1] = new Vector3(PointGrid[upperPointGridIndex + 1], (float)zQuad); // Upper Right
            quadPoints[2] = new Vector3(PointGrid[lowerPointGridIndex], (float)zQuad); // Lower Left
            quadPoints[3] = new Vector3(PointGrid[lowerPointGridIndex + 1], (float)zQuad); // Lower Right

            Vector2[] texturePoints = new Vector2[4];
            texturePoints[0] = new Vector2(0, 1);// Upper Left => quadPoints[1]
            texturePoints[1] = new Vector2(0, 0);// Lower Left => quadPoints[0]
            texturePoints[2] = new Vector2(1, 1);// Upper Right => quadPoints[3]
            texturePoints[3] = new Vector2(1, 0);// Lower Right => quadPoints[2]

            // int linearQuadId = (xQuad + yQuad * quadColumns);
            IIndexGenerator indexGenerator = new TriangleListIndexGenerator(quadId, pointColumns, pointRows);
            indexGenerator.Generate();
            var indices = indexGenerator.Points;
            
            QuadData quadData = new QuadData(quadBatchId, quadId, quadPoints, indices, texturePoints);
            quadId = IdManager.GetNextQuadId();
            return quadData;
        }

        public Vector3 GetQuadSizeInCoordinates()
        {
            return screenDivision.GetCoordinateUnitsPerQuad();
        }

        public Vector3 GetPixelSizeInCoordinates()
        {
            return screenDivision.GetCoordinateUnitsPerPixel();
        }

        public Vector3[] GetQuadAbsoluteCoords(Vector3[] quadPointsInGrid, float scale)
        {
            var leftUpper = this.GetAbsoluteCoord(quadPointsInGrid[0], scale);
            var leftDown = this.GetAbsoluteCoord(quadPointsInGrid[1], scale);
            var rightUpper = this.GetAbsoluteCoord(quadPointsInGrid[2], scale);
            var rightDown = this.GetAbsoluteCoord(quadPointsInGrid[3], scale);

            Vector3[] quadAbsoluteCoords = new Vector3[4]
            {
                leftDown, leftUpper , rightDown, rightUpper
            };

            return quadAbsoluteCoords;
        }

        private Vector3 GetAbsoluteCoord(Vector3 quadGridPoint, float quadScale)
        {
            var quadSizeInCoord = this.GetScaledQuadSizeInCoord(quadScale);
            var scaledPoint = new Vector3(quadGridPoint.X, -quadGridPoint.Y, quadGridPoint.Z) * new Vector3(quadSizeInCoord.X, quadSizeInCoord.Y, quadSizeInCoord.Z);
            var res = relativePosition + scaledPoint;
            return res;
        }

        private Vector3 GetScaledQuadSizeInCoord(float quadScale)
        {
            Vector3 quadSizeTemp = this.GetQuadSizeInCoordinates();
            var quadSizeInCoord = new Vector3(quadSizeTemp.X * quadScale, quadSizeTemp.Y * quadScale, quadSizeTemp.Z);
            return quadSizeInCoord;
        }
    }
}
