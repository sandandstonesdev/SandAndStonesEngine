using SandAndStonesEngine.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.MathModule
{
    public static class ScreenCartesianConverter
    {
        public static (int, int) CartesianToScreen(float cartesianX, float cartesianY, int screenWidth, int screenHeight)
        {
            int screenX = (int)(cartesianX * QuadGridManager.Instance.GetPixelsPerCoordinateUnit().X) + screenWidth / 2;
            int screenY = screenHeight / 2 - (int)(cartesianY * QuadGridManager.Instance.GetPixelsPerCoordinateUnit().Y);
            return (screenX, screenY);
        }

        public static (int, int) CartesianToScreenScrollMovement(float cartesianX, float cartesianY)
        {
            var unit = QuadGridManager.Instance.GetPixelsPerCoordinateUnit().X;

            int screenX = (int)(cartesianX * QuadGridManager.Instance.GetPixelsPerCoordinateUnit().X);
            int screenY = (int)(cartesianY * QuadGridManager.Instance.GetPixelsPerCoordinateUnit().Y);
            return (screenX, screenY);
        }

        public static (float, float) ScreenToCartesian(int screenX, int screenY, float cartesianWidth, float cartesianHeight)
        {
            var coordX = screenX * QuadGridManager.Instance.GetPixelSizeInCoordinates().X - cartesianWidth / 2;
            var coordY = cartesianHeight / 2 - screenY * QuadGridManager.Instance.GetPixelSizeInCoordinates().Y;
            Debug.WriteLine("ScreenToCartesian {0}", (coordX, coordY));
            return (coordX, coordY);
        }

        public static (float, float) ScreenToCartesianScrollMovement(int screenMovementX, int screenMovementY)
        {
            var coordX = screenMovementX * QuadGridManager.Instance.GetPixelSizeInCoordinates().X;
            var coordY = screenMovementY * QuadGridManager.Instance.GetPixelSizeInCoordinates().Y;
            Debug.WriteLine("ScreenToCartesian {0}", (coordX, coordY));
            return (coordX, coordY);
        }
    }
}
