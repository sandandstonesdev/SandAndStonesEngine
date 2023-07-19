using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public struct InstancedQuadData
    {
        public int id;
        public Vector2[] Points;
        public Vector2[] TextureCoords;
        public ushort[] Indexes = new ushort[6];
        public int TextureId;
        public InstancedQuadData(int id, Vector2[] points, ushort[] indexes, Vector2[] textureCoords, int textureId)
        {
            this.id = id;
            this.Points = points;
            this.Indexes = indexes;
            this.TextureCoords = textureCoords;
            this.TextureId = textureId;
        }
    }
}
