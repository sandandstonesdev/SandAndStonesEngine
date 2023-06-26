using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public struct QuadData
    {
        public int id;
        public Vector2[] Points;
        public ushort[] Indexes = new ushort[6];
        public QuadData(int id, Vector2[] points, ushort[] indexes)
        {
            this.id = id;
            this.Points = points;
            this.Indexes = indexes;
        }
    }
}
