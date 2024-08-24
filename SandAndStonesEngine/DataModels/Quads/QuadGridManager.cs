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
        private Vector2[] PointGrid;
        public ScreenDivisionForQuads screenDivision;
        private int quadId = 0;
        private int quadBatchId = 0;

        public int QuadId => quadId;
        public int QuadBatchId => quadBatchId;
        public int AbsoluteQuadIndex => quadBatchId * quadId + quadId;
        public Vector3 QuadCount => screenDivision.QuadCount;

        public QuadGridManager(ScreenDivisionForQuads screenDivision)
        {
            this.screenDivision = screenDivision;
            Init(screenDivision);
        }

        private void Init(ScreenDivisionForQuads screenDivision)
        {
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

        public QuadData GetQuadData(Vector2 screenPos, Vector3 gridQuadPosition, float scale, TileType tileType)
        {
            var texturePoints = new Vector2[4];
            texturePoints[0] = new Vector2(0, 0); // Upper Left => quadPoints[3]
            texturePoints[1] = new Vector2(1, 0); // Upper Right => quadPoints[1]
            texturePoints[2] = new Vector2(0, 1); // Lower Left => quadPoints[2]
            texturePoints[3] = new Vector2(1, 1); // Lower Right => quadPoints[0]

            Vector3[] quadPoints = new Vector3[4];
            int upperLeftPointGridIndex = CalculateQuadGridIndexForScreen(gridQuadPosition, screenDivision.QuadPointCount, texturePoints[0]);
            int upperRightPointGridIndex = CalculateQuadGridIndexForScreen(gridQuadPosition, screenDivision.QuadPointCount, texturePoints[1]);
            int lowerLeftPointGridIndex = CalculateQuadGridIndexForScreen(gridQuadPosition, screenDivision.QuadPointCount, texturePoints[2]);
            int lowerRightPointGridIndex = CalculateQuadGridIndexForScreen(gridQuadPosition, screenDivision.QuadPointCount, texturePoints[3]);

            // Reverse structure upside down because of difference of Grid and Axes representation:
            quadPoints[0] = new Vector3(PointGrid[upperRightPointGridIndex], gridQuadPosition.Z); // Lower Left
            quadPoints[1] = new Vector3(PointGrid[upperLeftPointGridIndex], gridQuadPosition.Z); // Lower Right
            quadPoints[2] = new Vector3(PointGrid[lowerRightPointGridIndex], gridQuadPosition.Z); // Upper Left
            quadPoints[3] = new Vector3(PointGrid[lowerLeftPointGridIndex], gridQuadPosition.Z); // Upper Right

            if (tileType == TileType.Background)
            {
                Debug.WriteLine("Index flow:");
                Debug.WriteLine($"{upperLeftPointGridIndex}, {upperRightPointGridIndex}");
                Debug.WriteLine($"{lowerLeftPointGridIndex}, {lowerRightPointGridIndex}");
            }

            IIndexGenerator indexGenerator = new TriangleListIndexGenerator(quadId);
            indexGenerator.Generate();
            var indices = indexGenerator.Points;

            var quadData = new QuadData(quadBatchId, quadId, screenPos, gridQuadPosition, scale, quadPoints, indices, texturePoints);
            quadId = IdManager.GetNextQuadId();
            return quadData;
        }
    }
}
