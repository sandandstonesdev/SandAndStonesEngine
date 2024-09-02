using SandAndStonesEngine.DataModels.ScreenDivisions;

namespace SandAndStonesEngine.MathModule
{
    public static class ScreenCartesianConverter
    {
        public static (int, int) CartesianToScreen(float cartesianX, float cartesianY, int screenWidth, int screenHeight)
        {
            int screenX = (int)(cartesianX * ScreenQuadCalculator.GetPixelsPerCoordinateUnit(screenWidth, screenHeight).X) + screenWidth / 2;
            int screenY = screenHeight / 2 - (int)(cartesianY * ScreenQuadCalculator.GetPixelsPerCoordinateUnit(screenWidth, screenHeight).Y);
            return (screenX, screenY);
        }

        public static (int, int) CartesianToScreenScrollMovement(float cartesianX, float cartesianY, int screenWidth, int screenHeight)
        {
            int screenX = (int)(cartesianX * ScreenQuadCalculator.GetPixelsPerCoordinateUnit(screenWidth, screenHeight).X);
            int screenY = (int)(cartesianY * ScreenQuadCalculator.GetPixelsPerCoordinateUnit(screenWidth, screenHeight).Y);
            return (screenX, screenY);
        }

        public static (float, float) ScreenToCartesian(int screenX, int screenY, float cartesianWidth, float cartesianHeight, int screenWidth, int screenHeight, int screenDepth)
        {
            var coordX = screenX * ScreenQuadCalculator.GetCoordinateUnitsPerPixel(screenWidth, screenHeight, screenDepth).X - cartesianWidth / 2;
            var coordY = cartesianHeight / 2 - screenY * ScreenQuadCalculator.GetCoordinateUnitsPerPixel(screenWidth, screenHeight, screenDepth).Y;
            //Debug.WriteLine("ScreenToCartesian {0}", (coordX, coordY));
            return (coordX, coordY);
        }

        public static (float, float) ScreenToCartesianScrollMovement(int screenMovementX, int screenMovementY, int screenWidth, int screenHeight, int screenDepth)
        {
            var coordX = screenMovementX * ScreenQuadCalculator.GetCoordinateUnitsPerPixel(screenWidth, screenHeight, screenDepth).X;
            var coordY = screenMovementY * ScreenQuadCalculator.GetCoordinateUnitsPerPixel(screenWidth, screenHeight, screenDepth).Y;
            //Debug.WriteLine("ScreenToCartesian {0}", (coordX, coordY));
            return (coordX, coordY);
        }
    }
}
