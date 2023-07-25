using SandAndStonesEngine.Buffers;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{

    public class QuadModel
    {
        private VertexDataFormat[] verticesPositions = new VertexDataFormat[4];
        private Vector3 relativePosition = new Vector3(-1.00f, 1.00f, 0.0f);
        private Vector3 quadSizeInCoord;
        private Vector3 pixelSizeInCoord;

        private RgbaFloat color;
        private Vector3[] quadPointsInGrid;
        private Vector2[] quadTextureCoords;
        private ushort[] quadIndexesInGrid;
        private int textureId;
        public VertexDataFormat[] VerticesPositions
        {
            get { return verticesPositions; }
        }
        public ushort[] QuadIndexes
        {
            get { return quadIndexesInGrid; }
        }
        public QuadModel(Vector3 gridQuadPosition, float quadScale, RgbaFloat color, QuadGrid quadGrid, int textureId)
        {
            QuadData quadData = quadGrid.GetQuadData((int)gridQuadPosition.X, (int)gridQuadPosition.Y, (int)gridQuadPosition.Z);
            this.quadPointsInGrid = quadData.Points;
            this.quadIndexesInGrid = quadData.Indexes;
            this.quadTextureCoords = quadData.TextureCoords;
            this.textureId = textureId;// quadData.TextureId;
            Vector3 quadSizeTemp = quadGrid.GetQuadSizeInCoordinates();
            this.quadSizeInCoord = new Vector3(quadSizeTemp.X * quadScale, quadSizeTemp.Y * quadScale, quadSizeTemp.Z);
            this.pixelSizeInCoord = quadGrid.GetPixelSizeInCoordinates();
            this.color = color;
        }
        
        private Vector3 GetAbsoluteCoord(Vector3 quadGridPoint)
        {
            var scaledPoint = new Vector3(quadGridPoint.X, -quadGridPoint.Y, quadGridPoint.Z) * new Vector3(quadSizeInCoord.X, quadSizeInCoord.Y, quadSizeInCoord.Z);
            var res = relativePosition + scaledPoint;
            return res;
        }

        public void Create()
        {
            var leftUpper = GetAbsoluteCoord(quadPointsInGrid[0]); //quadPointsInCoord[0];
            var leftDown = GetAbsoluteCoord(quadPointsInGrid[1]);
            var rightUpper = GetAbsoluteCoord(quadPointsInGrid[2]);
            var rightDown = GetAbsoluteCoord(quadPointsInGrid[3]);

            verticesPositions[0] = new VertexDataFormat(leftDown, color, quadTextureCoords[0], textureId);
            verticesPositions[1] = new VertexDataFormat(leftUpper, color, quadTextureCoords[1], textureId);
            verticesPositions[2] = new VertexDataFormat(rightDown, color, quadTextureCoords[2], textureId);
            verticesPositions[3] = new VertexDataFormat(rightUpper, color, quadTextureCoords[3], textureId);
        }

        public void Move(Vector2 pixelMovementVector)
        {
            var movement = new Vector2(pixelMovementVector.X * pixelSizeInCoord.X, pixelMovementVector.Y * pixelSizeInCoord.Y);
            for (int i = 0; i < verticesPositions.Length; i++)
            {
                verticesPositions[i].Position = 
                    new Vector4(verticesPositions[i].Position.X + movement.X, 
                                verticesPositions[i].Position.Y + movement.Y,
                                verticesPositions[i].Position.Z,
                                verticesPositions[i].Position.W);
            }
        }
    }
}
