using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class ScreenDivisionForQuads
    {
        int screenWidth;
        int screenHeight;
        int pointCountX;
        public int PointCountX => pointCountX;
        int pointCountY;
        public int PointCountY => pointCountY;
        public float aspect;
        public ScreenDivisionForQuads(int screenWidth, int screenHeight, int pointCountX, int pointCountY)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.pointCountX = pointCountX;
            this.pointCountY = pointCountY;
            this.aspect = screenWidth / screenHeight;
        }

        public Vector2 GetCoordinateUnitsPerQuad()
        {
            int quadCountX = pointCountX / 2;
            int quadCountY = pointCountY / 2;
            float xCount = 2.0f / quadCountX;
            float yCount = 2.0f / quadCountY;
            return new Vector2(xCount, yCount);
        }
        public Vector2 GetPixelUnitsPerQuad()
        {
            int xQuadSizeInPixels = screenWidth / pointCountX;
            int yQuadSizeInPixels = screenHeight / pointCountY;
            return new Vector2(xQuadSizeInPixels, yQuadSizeInPixels);
        }
    }
}
