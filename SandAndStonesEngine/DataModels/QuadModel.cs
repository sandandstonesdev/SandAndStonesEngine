﻿using SandAndStonesEngine.Buffers;
using System.Diagnostics;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels
{
    public class QuadModel
    {
        private VertexDataFormat[] verticesPositions = new VertexDataFormat[0];
        private Vector2 relativePosition = new Vector2(-1.00f, 1.00f);
        private Vector2 quadSizeInCoord;

        private RgbaFloat color;
        private Vector2[] quadPointsInGrid;
        private Vector2[] quadTextureCoords;
        private ushort[] quadIndexesInGrid;
        public VertexDataFormat[] VerticesPositions
        {
            get { return verticesPositions; }
        }
        public ushort[] QuadIndexes
        {
            get { return quadIndexesInGrid; }
        }
        public QuadModel(Vector2 gridQuadPosition, RgbaFloat color, QuadGrid quadGrid)
        {
            QuadData quadData = quadGrid.GetQuadData((int)gridQuadPosition.X, (int)gridQuadPosition.Y);

            this.quadPointsInGrid = quadData.Points;
            this.quadIndexesInGrid = quadData.Indexes;
            this.quadTextureCoords = quadData.TextureCoords;
            this.quadSizeInCoord = quadGrid.GetQuadSizeInCoordinates();
            this.color = color;;
        }
        
        
        private Vector2 GetAbsoluteCoord(Vector2 quadGridPoint)
        {
            var scaledPoint = new Vector2(quadGridPoint.X, -quadGridPoint.Y) * new Vector2(quadSizeInCoord.X, quadSizeInCoord.Y);
            var res = relativePosition + scaledPoint;
            //res = scaledPoint;
            //Debug.WriteLine($"{ res.X} {res.Y}");
            return res;
        }
        public void Init()
        {
            var leftUpper = GetAbsoluteCoord(quadPointsInGrid[0]); //quadPointsInCoord[0];
            var leftDown = GetAbsoluteCoord(quadPointsInGrid[1]);
            var rightUpper = GetAbsoluteCoord(quadPointsInGrid[2]);
            var rightDown = GetAbsoluteCoord(quadPointsInGrid[3]);
            
            verticesPositions = new[]
            {
                new VertexDataFormat(new Vector3(leftDown, 0.0f), color, quadTextureCoords[0]),
                new VertexDataFormat(new Vector3(leftUpper, 0.0f), color, quadTextureCoords[1]),
                new VertexDataFormat(new Vector3(rightDown, 0.0f), color, quadTextureCoords[2]),
                new VertexDataFormat(new Vector3(rightUpper, 0.0f), color, quadTextureCoords[3]),
            };
        }
    }
}
