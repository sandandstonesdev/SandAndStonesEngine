﻿using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public struct QuadData
    {
        public int Id;
        public Vector3[] Points;
        public Vector2[] TextureCoords;
        public ushort[] Indexes = new ushort[6];
        public int TextureId = 0;
        public QuadData(int id, Vector3[] points, ushort[] indexes, Vector2[] textureCoords)
        {
            this.Id = id;
            this.TextureId = id;
            this.Points = points;
            this.Indexes = indexes;
            this.TextureCoords = textureCoords;
        }
    }
}
