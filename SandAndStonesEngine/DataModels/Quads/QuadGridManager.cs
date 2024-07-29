using SandAndStonesEngine.Assets;
using SandAndStonesEngine.DataModels.ScreenDivisions;
using SandAndStonesEngine.Utils;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace SandAndStonesEngine.DataModels.Quads
{
    public class QuadGridManager
    {
        private static readonly Lazy<QuadGridManager> lazyInstance = new Lazy<QuadGridManager>(() => new QuadGridManager());
        public static QuadGridManager Instance => lazyInstance.Value;

        private Vector3 relativePosition = new Vector3(-1.00f, -1.00f, 0.0f);
        Vector2[] PointGrid;
        int pointColumns;
        int pointRows;
        ScreenDivisionForQuads screenDivision;
        int quadId = 0;
        int quadBatchId = 0;

        public int QuadId => quadId;
        public int QuadBatchId => quadBatchId;
        public int AbsoluteQuadIndex => quadBatchId * quadId + quadId;

        public QuadGridManager()
        {

        }

        public void Init(ScreenDivisionForQuads screenDivision)
        {
            this.screenDivision = screenDivision;
            pointColumns = screenDivision.QuadCountX + 1;
            pointRows = screenDivision.QuadCountY + 1;
            IVertexGenerator vertexGenerator = new VertexGenerator(pointRows, pointColumns);
            vertexGenerator.Generate();
            PointGrid = vertexGenerator.Points;
            quadId = 0;
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

        public int CalculateQuadGridIndex(int xQuad, int yQuad, int xQuadSize, int yQuadSize)
        {
            int idx = yQuadSize - yQuad - 1 + xQuad * xQuadSize - 1;
            return idx;
        }

        public QuadData GetQuadData(Vector3 quadVector)
        {
            Vector3[] quadPoints = new Vector3[4];
            int lowerPointGridIndex = CalculateQuadGridIndex((int)quadVector.X, (int)quadVector.Y, screenDivision.QuadCountX + 1, screenDivision.QuadCountY + 1);
            int upperPointGridIndex = CalculateQuadGridIndex((int)quadVector.X + 1, (int)quadVector.Y, screenDivision.QuadCountX + 1, screenDivision.QuadCountY + 1);
            quadPoints[0] = new Vector3(PointGrid[upperPointGridIndex], quadVector.Z); // Lower Left
            quadPoints[1] = new Vector3(PointGrid[upperPointGridIndex + 1], quadVector.Z); // Lower Right
            quadPoints[2] = new Vector3(PointGrid[lowerPointGridIndex], quadVector.Z); // Upper Left
            quadPoints[3] = new Vector3(PointGrid[lowerPointGridIndex + 1], quadVector.Z); // Upper Right

            Vector2[] texturePoints = new Vector2[4];
            texturePoints[0] = new Vector2(1, 0);// Upper Right => quadPoints[3]
            texturePoints[1] = new Vector2(1, 1);// Lower Right => quadPoints[1]
            texturePoints[2] = new Vector2(0, 0);// Upper Left => quadPoints[2]
            texturePoints[3] = new Vector2(0, 1);// Lower Left => quadPoints[0]

            // int linearQuadId = (xQuad + yQuad * quadColumns);
            IIndexGenerator indexGenerator = new TriangleListIndexGenerator(quadId, pointRows, pointColumns);
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

        public Vector2 GetPixelsPerCoordinateUnit()
        {
            return screenDivision.GetPixelsPerCoordinateUnit();
        }

        public Vector3[] GetQuadAbsoluteCoords(Vector3[] quadPointsInGrid, float scale)
        {
            var leftUpper = GetAbsoluteCoord(quadPointsInGrid[0], scale);
            var leftDown = GetAbsoluteCoord(quadPointsInGrid[1], scale);
            var rightUpper = GetAbsoluteCoord(quadPointsInGrid[2], scale);
            var rightDown = GetAbsoluteCoord(quadPointsInGrid[3], scale);

            Vector3[] quadAbsoluteCoords = new Vector3[4]
            {
                leftDown, leftUpper, rightDown, rightUpper
            };

            return quadAbsoluteCoords;
        }

        private Vector3 GetAbsoluteCoord(Vector3 quadGridPoint, float quadScale)
        {
            var quadSizeInCoord = GetScaledQuadSizeInCoord(quadScale);
            var scaledPoint = Vector3.Multiply(quadGridPoint, quadSizeInCoord);
            var res = relativePosition + scaledPoint;
            return res;
        }

        private Vector3 GetScaledQuadSizeInCoord(float quadScale)
        {
            Vector3 quadSizeTemp = GetQuadSizeInCoordinates();
            var quadSizeInCoord = Vector3.Multiply(quadSizeTemp, new Vector3(quadScale, quadScale, 1));
            return quadSizeInCoord;
        }
    }
}
