using System.Numerics;

namespace SandAndStonesEngine.DataModels.ScreenDivisions
{
    public class ScreenQuadCalculator
    {
        private int screenWidth;
        private int screenHeight;

        public int ScreenWidth => screenWidth;
        public int ScreenHeight => screenHeight;

        private readonly int screenDepth;
        public Vector3 QuadCount { get; init; }
        public Vector3 QuadPointCount => QuadCount + Vector3.One;
        public float aspect;
        public ScreenQuadCalculator(int screenWidth, int screenHeight, int quadCountX, int quadCountY, int quadCountZ = 10)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.QuadCount = new Vector3(quadCountX, quadCountY, quadCountZ);
            aspect = screenWidth / screenHeight;
            screenDepth = screenWidth;
        }

        public void Resize(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public Vector3 GetCoordinateUnitsPerQuad()
        {
            float xCount = 2.0f / QuadCount.X;
            float yCount = 2.0f / QuadCount.Y;
            float zCount = 2.0f / QuadCount.Z;
            return new Vector3(xCount, yCount, zCount);
        }

        public static Vector3 GetCoordinateUnitsPerPixel(int screenWidth, int screenHeight, int screenDepth)
        {
            float xCount = 2.0f / screenWidth;
            float yCount = 2.0f / screenHeight;
            float zCount = 2.0f / screenDepth;
            return new Vector3(xCount, yCount, zCount);
        }

        public static Vector2 GetPixelsPerCoordinateUnit(int screenWidth, int screenHeight)
        {
            int xPixels = screenWidth / 2;
            int yPixels = screenHeight / 2;
            return new Vector2(xPixels, yPixels);
        }

        public Vector3 GetPixelUnitsPerQuad()
        {
            var xQuadSizeInPixels = screenWidth / QuadCount.X;
            var yQuadSizeInPixels = screenHeight / QuadCount.Y;
            var zQuadSizeInPixels = screenDepth / QuadCount.Z;
            return new Vector3(xQuadSizeInPixels, yQuadSizeInPixels, zQuadSizeInPixels);
        }
    }
}
