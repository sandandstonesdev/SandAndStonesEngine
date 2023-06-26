using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class ScreenDivisionForQuads
    {
        int screenWidth;
        int screenHeight;
        int quadCountX;
        public int QuadCountX => quadCountX;
        int quadCountY;
        public int QuadCountY => quadCountY;
        public ScreenDivisionForQuads(int screenWidth, int screenHeight, int quadCountX, int quadCountY)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.quadCountX = quadCountX;
            this.quadCountY = quadCountY;
        }

        public Vector2 GetCoordinateUnitsPerQuad()
        {
            float xCount = 1.0f / quadCountX;
            float yCount = 1.0f / quadCountY;
            return new Vector2(xCount, yCount);
        }
        public Vector2 GetPixelUnitsPerQuad()
        {
            int xQuadSizeInPixels = screenWidth / quadCountX;
            int yQuadSizeInPixels = screenHeight / quadCountY;
            return new Vector2(xQuadSizeInPixels, yQuadSizeInPixels);
        }
    }
}
