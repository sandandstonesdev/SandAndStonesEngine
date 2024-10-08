﻿using System.Numerics;

namespace SandAndStonesEngine.DataModels.Quads
{
    public struct QuadData
    {
        public int Id;
        public Vector2 ScreenPos;
        public Vector3 GridQuadPosition;
        public Vector3[] Points;
        public Vector2[] TextureCoords;
        public ushort[] Indexes = new ushort[6];
        public int TextureId = 0;
        public int BatchId;
        public float Scale;

        public QuadData(int batchId, int id, Vector2 screenPos, Vector3 gridQuadPosition, float scale, Vector3[] points, ushort[] indexes, Vector2[] textureCoords)
        {
            BatchId = batchId;
            Id = id;
            TextureId = id;
            ScreenPos = screenPos;
            GridQuadPosition = gridQuadPosition;
            Scale = scale;
            Points = points;
            Indexes = indexes;
            TextureCoords = textureCoords;
        }
    }
}
