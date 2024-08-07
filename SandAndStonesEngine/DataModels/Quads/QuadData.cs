﻿using System.Numerics;

namespace SandAndStonesEngine.DataModels.Quads
{
    public struct QuadData
    {
        public int Id;
        public Vector3[] Points;
        public Vector2[] TextureCoords;
        public ushort[] Indexes = new ushort[6];
        public int TextureId = 0;
        public int BatchId;
        public QuadData(int batchId, int id, Vector3[] points, ushort[] indexes, Vector2[] textureCoords)
        {
            BatchId = batchId;
            Id = id;
            TextureId = id;
            Points = points;
            Indexes = indexes;
            TextureCoords = textureCoords;
        }
    }
}
