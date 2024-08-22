using SandAndStonesEngine.Assets;
using SandAndStonesEngine.DataModels.ScreenDivisions;
using SandAndStonesEngine.DataModels.Tiles;
using SandAndStonesEngine.Utils;
using System.Diagnostics;
using System.Numerics;

namespace SandAndStonesEngine.DataModels.Quads
{
    public class QuadGridManager
    {
        private static readonly Lazy<QuadGridManager> lazyInstance = new Lazy<QuadGridManager>(() => new QuadGridManager());
        public static QuadGridManager Instance => lazyInstance.Value;

        private Vector3 relativePosition = new Vector3(-1.00f, -1.00f, 0.0f);
        private Vector2[] PointGrid;
        private ScreenDivisionForQuads screenDivision;
        private int quadId = 0;
        private int quadBatchId = 0;

        public int QuadId => quadId;
        public int QuadBatchId => quadBatchId;
        public int AbsoluteQuadIndex => quadBatchId * quadId + quadId;
        public Vector3 QuadCount => screenDivision.QuadCount;

        public QuadGridManager()
        {

        }

        public void Init(ScreenDivisionForQuads screenDivision)
        {
            this.screenDivision = screenDivision;
            IVertexGenerator vertexGenerator = new VertexGenerator((int)screenDivision.QuadPointCount.Y, (int)screenDivision.QuadPointCount.X);
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

        public int CalculateQuadGridIndexForScreen(Vector3 quadGridPos, Vector3 quadPointCount, Vector2 offset)
        {
            var idxX = (quadGridPos.X + offset.X) * quadPointCount.X;

            var idxY = quadPointCount.Y - (quadGridPos.Y + offset.Y) - 1; // Minus from quadSize.Y because y Coord layout is reverse than Grid Ys
            // If we adding screenPos.Y calculations we have to have number of screens x, y parameter
            var idx = idxX + idxY;
            return (int)idx;
        }

        public QuadData GetQuadData(Vector2 screenVector, Vector3 quadVector, TileType tileType)
        {
            Vector2[] texturePoints = new Vector2[4];
            texturePoints[0] = new Vector2(0, 0); // Upper Left => quadPoints[3]
            texturePoints[1] = new Vector2(1, 0); // Upper Right => quadPoints[1]
            texturePoints[2] = new Vector2(0, 1); // Lower Left => quadPoints[2]
            texturePoints[3] = new Vector2(1, 1); // Lower Right => quadPoints[0]

            Vector3[] quadPoints = new Vector3[4];
            int upperLeftPointGridIndex = CalculateQuadGridIndexForScreen(quadVector, screenDivision.QuadPointCount, texturePoints[0]);
            int upperRightPointGridIndex = CalculateQuadGridIndexForScreen(quadVector, screenDivision.QuadPointCount, texturePoints[1]);
            int lowerLeftPointGridIndex = CalculateQuadGridIndexForScreen(quadVector, screenDivision.QuadPointCount, texturePoints[2]);
            int lowerRightPointGridIndex = CalculateQuadGridIndexForScreen(quadVector, screenDivision.QuadPointCount, texturePoints[3]);

            // Reverse structure upside down because of difference of Grid and Axes representation:
            quadPoints[0] = new Vector3(PointGrid[upperRightPointGridIndex], quadVector.Z); // Lower Left
            quadPoints[1] = new Vector3(PointGrid[upperLeftPointGridIndex], quadVector.Z); // Lower Right
            quadPoints[2] = new Vector3(PointGrid[lowerRightPointGridIndex], quadVector.Z); // Upper Left
            quadPoints[3] = new Vector3(PointGrid[lowerLeftPointGridIndex], quadVector.Z); // Upper Right

            if (tileType == TileType.Background)
            {
                Debug.WriteLine("Index flow:");
                Debug.WriteLine($"{upperLeftPointGridIndex}, {upperRightPointGridIndex}");
                Debug.WriteLine($"{lowerLeftPointGridIndex}, {lowerRightPointGridIndex}");
            }
            
            IIndexGenerator indexGenerator = new TriangleListIndexGenerator(quadId);
            indexGenerator.Generate();
            var indices = indexGenerator.Points;

            QuadData quadData = new QuadData(quadBatchId, quadId, screenVector, quadPoints, indices, texturePoints);
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
