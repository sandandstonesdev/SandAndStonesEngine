using System.Numerics;

namespace SandAndStonesEngine.Utils
{
    public interface IVertexGenerator
    {

        public Vector2[] Points { get; }
        public void Generate();
    }
}