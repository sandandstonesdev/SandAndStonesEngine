using SandAndStonesEngine.Utils;
using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class QuadGrid
    {
        readonly Vector2[] PointGrid;
        readonly int pointColumns;
        readonly int pointRows;
        ScreenDivisionForQuads screenDivision;
        int quadId = 0;
        ushort[] indexesPattern = new ushort[] { 0, 1, 2, 1, 3, 2 };
        public QuadGrid(ScreenDivisionForQuads screenDivision)
        {
            this.screenDivision = screenDivision;
            this.pointColumns = screenDivision.PointCountX;
            this.pointRows = screenDivision.PointCountY;
            IVertexGenerator vertexGenerator = new VertexGenerator(pointColumns, pointRows);
            vertexGenerator.Generate();
            this.PointGrid = vertexGenerator.Points;
            this.quadId = 0;
        }

        public QuadData GetQuadData(int xQuad, int yQuad)
        {
            Vector2[] quadPoints = new Vector2[4];
            int upperPointGridIndex = (yQuad + xQuad * pointColumns);
            int lowerPointGridIndex = (yQuad + (xQuad + 1) * pointRows);
            quadPoints[0] = PointGrid[upperPointGridIndex]; // Upper Left
            quadPoints[1] = PointGrid[upperPointGridIndex + 1]; // Upper Right
            quadPoints[2] = PointGrid[lowerPointGridIndex]; // Lower Left
            quadPoints[3] = PointGrid[lowerPointGridIndex + 1]; // Lower Right

            //// OpenGL
            Vector2[] texturePoints = new Vector2[4];
            texturePoints[0] = new Vector2(0, 1);// Upper Left => quadPoints[1]
            texturePoints[1] = new Vector2(0, 0);// Lower Left => quadPoints[0]
            texturePoints[2] = new Vector2(1, 1);// Upper Right => quadPoints[3]
            texturePoints[3] = new Vector2(1, 0);// Lower Right => quadPoints[2]

            // int linearQuadId = (xQuad + yQuad * quadColumns);
            IIndexGenerator indexGenerator = new TriangleListIndexGenerator(quadId, pointColumns, pointRows);
            indexGenerator.Generate();
            var indices = indexGenerator.Points;
            quadId++;
            QuadData quadData = new QuadData(quadId, quadPoints, indices, texturePoints);
            return quadData;
        }

        public Vector2 GetQuadSizeInCoordinates()
        {
            return screenDivision.GetCoordinateUnitsPerQuad();
        }
    }
}
