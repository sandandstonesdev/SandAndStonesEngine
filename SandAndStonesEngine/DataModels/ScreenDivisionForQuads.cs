using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class ScreenDivisionForQuads
    {
        int screenWidth;
        int screenHeight;
        int screenDepth;
        int pointCountX;
        public int PointCountX => pointCountX;
        int pointCountY;
        public int PointCountY => pointCountY;
        int pointCountZ; // available Z layers
        public int PointCountZ => pointCountZ;

        public float aspect;
        public ScreenDivisionForQuads(int screenWidth, int screenHeight, int pointCountX, int pointCountY, int pointCountZ = 10)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.pointCountX = pointCountX;
            this.pointCountY = pointCountY;
            this.pointCountZ = pointCountZ;
            this.aspect = screenWidth / screenHeight;
            this.screenDepth = screenWidth;
        }

        public void Resize(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public Vector3 GetCoordinateUnitsPerQuad()
        {
            int quadCountX = pointCountX / 2;
            int quadCountY = pointCountY / 2;
            int quadCountZ = pointCountZ / 2;
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
        public Vector3 GetPixelUnitsPerQuad()
        {
            int xQuadSizeInPixels = screenWidth / pointCountX;
            int yQuadSizeInPixels = screenHeight / pointCountY;
            int zQuadSizeInPixels = screenDepth / pointCountY;
            return new Vector3(xQuadSizeInPixels, yQuadSizeInPixels, zQuadSizeInPixels);
        }
    }
}
