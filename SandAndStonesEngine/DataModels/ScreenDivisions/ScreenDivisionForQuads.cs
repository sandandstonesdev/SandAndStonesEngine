using SandAndStonesEngine.DataModels.Quads;
using System.Numerics;

namespace SandAndStonesEngine.DataModels.ScreenDivisions
{
    public class ScreenDivisionForQuads
    {
        private int screenWidth;
        private int screenHeight;
        private int screenDepth;
        public Vector3 QuadCount { get; init; }
        public Vector3 QuadPointCount { get; init; }

        public float aspect;
        public ScreenDivisionForQuads(int screenWidth, int screenHeight, int quadCountX, int quadCountY, int quadCountZ = 10)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.QuadCount = new Vector3(quadCountX, quadCountY, quadCountZ);
            this.QuadPointCount = new Vector3(quadCountX + 1, quadCountY + 1, quadCountZ + 1);
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

        public Vector3 GetCoordinateUnitsPerPixel()
        {
            float xCount = 2.0f / screenWidth;
            float yCount = 2.0f / screenHeight;
            float zCount = 2.0f / screenDepth;
            return new Vector3(xCount, yCount, zCount);
        }

        public Vector2 GetPixelsPerCoordinateUnit()
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
