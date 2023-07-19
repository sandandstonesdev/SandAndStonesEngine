using SandAndStonesEngine.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class InstancedQuadModel : IQuadModel
    {
        private IVertexFormat[] verticesPositions = new IVertexFormat[4];
        private Vector2 relativePosition = new Vector2(-1.00f, 1.00f);
        private Vector3 quadSizeInCoord;

        private RgbaFloat color;
        private Vector3[] quadPointsInGrid;
        private Vector2[] quadTextureCoords;
        private ushort[] quadIndexesInGrid;
        private float quadScale;
        public IVertexFormat[] VerticesPositions
        {
            get { return verticesPositions; }
        }
        public ushort[] QuadIndexes
        {
            get { return quadIndexesInGrid; }
        }

        public int TextureId;
        public InstancedQuadModel(int textureId, Vector2 gridQuadPosition, float quadScale, RgbaFloat color, QuadGrid quadGrid)
        {
            QuadData quadData = quadGrid.GetQuadData((int)gridQuadPosition.X, (int)gridQuadPosition.Y, 0);

            this.quadPointsInGrid = quadData.Points;
            this.quadIndexesInGrid = quadData.Indexes;
            this.quadTextureCoords = quadData.TextureCoords;
            this.quadScale = quadScale;
            this.quadSizeInCoord = quadGrid.GetQuadSizeInCoordinates() * quadScale;
            this.color = color;
            this.TextureId = textureId;
        }


        private Vector3 GetAbsoluteCoord(Vector3 quadGridPoint)
        {
            var scaledPoint = new Vector2(quadGridPoint.X, -quadGridPoint.Y) * new Vector2(quadSizeInCoord.X, quadSizeInCoord.Y);
            var res = relativePosition + scaledPoint;
            //res = scaledPoint;
            //Debug.WriteLine($"{ res.X} {res.Y}");
            return new Vector3(res, quadGridPoint.X);
        }
        public void Create()
        {
            var leftUpper = GetAbsoluteCoord(quadPointsInGrid[0]); //quadPointsInCoord[0];
            var leftDown = GetAbsoluteCoord(quadPointsInGrid[1]);
            var rightUpper = GetAbsoluteCoord(quadPointsInGrid[2]);
            var rightDown = GetAbsoluteCoord(quadPointsInGrid[3]);

            verticesPositions[0] = new VertexInstanceDataFormat(leftDown, color, quadTextureCoords[0], TextureId);
            verticesPositions[1] = new VertexInstanceDataFormat(leftUpper, color, quadTextureCoords[1], TextureId);
            verticesPositions[2] = new VertexInstanceDataFormat(rightDown, color, quadTextureCoords[2], TextureId);
            verticesPositions[3] = new VertexInstanceDataFormat(rightUpper, color, quadTextureCoords[3], TextureId);
        }
    }
}
