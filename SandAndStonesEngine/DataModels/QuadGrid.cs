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
            
            QuadData quadData = new QuadData(quadId, quadPoints, indices, texturePoints);
            quadId++;
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
    }
}
