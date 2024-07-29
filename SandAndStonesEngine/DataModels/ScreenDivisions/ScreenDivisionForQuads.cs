using System.Numerics;

namespace SandAndStonesEngine.DataModels.ScreenDivisions
{
    public class ScreenDivisionForQuads
    {
        int screenWidth;
        int screenHeight;
        int screenDepth;
        int quadCountX;
        public int QuadCountX => quadCountX;
        int quadCountY;
        public int QuadCountY => quadCountY;
        int quadCountZ; // available Z layers
        public int QuadCountZ => quadCountZ;

        public float aspect;
        public ScreenDivisionForQuads(int screenWidth, int screenHeight, int quadCountX, int quadCountY, int quadCountZ = 10)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.quadCountX = quadCountX;
            this.quadCountY = quadCountY;
            this.quadCountZ = quadCountZ;
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
            float xCount = 2.0f / quadCountX;
            float yCount = 2.0f / quadCountY;
            float zCount = 2.0f / quadCountZ;
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
            int xQuadSizeInPixels = screenWidth / quadCountX;
            int yQuadSizeInPixels = screenHeight / quadCountY;
            int zQuadSizeInPixels = screenDepth / quadCountZ;
            return new Vector3(xQuadSizeInPixels, yQuadSizeInPixels, zQuadSizeInPixels);
        }
    }
}
