﻿using SandAndStonesEngine.Buffers;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.DataModels.Quads
{
    public class QuadModel : IQuadModel, IVisibleModel
    {
        int quadId;
        int quadBatchId;
        private RgbaFloat color;
        private Vector3[] quadPointsInGrid;
        private Vector2[] quadTextureCoords;
        private ushort[] quadIndexesInGrid;
        private int textureId;
        private uint assetId;
        protected VertexDataFormat[] verticesPositions = new VertexDataFormat[4];

        public VertexDataFormat[] VerticesPositions
        {
            get { return verticesPositions; }
        }

        public bool Visible
        {
            get;
        }

        public ushort[] QuadIndexes
        {
            get { return quadIndexesInGrid; }
        }

        public float scale;

        public QuadModel(Vector3 gridQuadPosition, float quadScale, RgbaFloat color, uint assetId, int textureId)
        {
            QuadData quadData = QuadGridManager.Instance.GetQuadData(gridQuadPosition);
            quadPointsInGrid = quadData.Points;
            quadIndexesInGrid = quadData.Indexes;
            quadTextureCoords = quadData.TextureCoords;
            quadId = quadData.Id;
            quadBatchId = quadData.BatchId;
            this.assetId = assetId;
            this.textureId = textureId;
            this.color = color;
            scale = quadScale;
        }

        public void Init()
        {
            var quadAbsoluteCoords = QuadGridManager.Instance.GetQuadAbsoluteCoords(quadPointsInGrid, scale);

            verticesPositions[0] = new VertexDataFormat(quadAbsoluteCoords[0], color, quadTextureCoords[0], assetId, textureId);
            verticesPositions[1] = new VertexDataFormat(quadAbsoluteCoords[1], color, quadTextureCoords[1], assetId, textureId);
            verticesPositions[2] = new VertexDataFormat(quadAbsoluteCoords[2], color, quadTextureCoords[2], assetId, textureId);
            verticesPositions[3] = new VertexDataFormat(quadAbsoluteCoords[3], color, quadTextureCoords[3], assetId, textureId);
        }

        public virtual void Move(Vector4 movement)
        {
            for (int i = 0; i < verticesPositions.Length; i++)
            {
                verticesPositions[i].Position =
                    Vector4.Add(verticesPositions[i].Position, movement);
            }
        }

        public bool IsVisible(ScrollableViewport scrollableViewport)
        {
            return scrollableViewport.ContainsVertex(verticesPositions[0].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[1].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[2].Position)
            || scrollableViewport.ContainsVertex(verticesPositions[3].Position);
        }
    }
}
