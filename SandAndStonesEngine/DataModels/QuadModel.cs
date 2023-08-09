using SandAndStonesEngine.Buffers;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class QuadModel : IQuadModel
    {
        int quadId;
        int quadBatchId;
        private RgbaFloat color;
        private Vector3[] quadPointsInGrid;
        private Vector2[] quadTextureCoords;
        private ushort[] quadIndexesInGrid;
        private int textureId;
        private uint assetId;
        private VertexDataFormat[] verticesPositions = new VertexDataFormat[4];

        public VertexDataFormat[] VerticesPositions
        {
            get { return verticesPositions; }
        }
        public ushort[] QuadIndexes
        {
            get { return quadIndexesInGrid; }
        }

        public float scale;
        public QuadModel(Vector3 gridQuadPosition, float quadScale, RgbaFloat color, uint assetId, int textureId)
        {
            QuadData quadData = QuadGridManager.Instance.GetQuadData((int)gridQuadPosition.X, (int)gridQuadPosition.Y, (int)gridQuadPosition.Z);
            this.quadPointsInGrid = quadData.Points;
            this.quadIndexesInGrid = quadData.Indexes;
            this.quadTextureCoords = quadData.TextureCoords;
            this.quadId = quadData.Id;
            this.quadBatchId = quadData.BatchId;
            this.assetId = assetId;
            this.textureId = textureId;
            this.color = color;
            this.scale = quadScale;
        }
        
        public void Init()
        {
            var quadAbsoluteCoords = QuadGridManager.Instance.GetQuadAbsoluteCoords(quadPointsInGrid, scale);

            verticesPositions[0] = new VertexDataFormat(quadAbsoluteCoords[0], color, quadTextureCoords[0], (uint)assetId, textureId);
            verticesPositions[1] = new VertexDataFormat(quadAbsoluteCoords[1], color, quadTextureCoords[1], (uint)assetId, textureId);
            verticesPositions[2] = new VertexDataFormat(quadAbsoluteCoords[2], color, quadTextureCoords[2], (uint)assetId, textureId);
            verticesPositions[3] = new VertexDataFormat(quadAbsoluteCoords[3], color, quadTextureCoords[3], (uint)assetId, textureId);
        }

        protected void Move(Vector2 pixelMovementVector)
        {
            var pixelSizeInCoord = QuadGridManager.Instance.GetPixelSizeInCoordinates();
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
