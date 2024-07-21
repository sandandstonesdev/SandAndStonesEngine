using SandAndStonesEngine.MathModule;
using SharpGen.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.DataModels
{
    public class ScrollableViewport
    {
        private readonly int X, Y, Width, Height;
        private int ScrollX = 0, ScrollY = 0;
        private const float CartesianWidth = 2.0f;
        private const float CartesianHeight = 2.0f;
        public (float, float) CartesianCoords
        {
            get
            {
                return ScreenCartesianConverter.ScreenToCartesianScrollMovement(ScrollX, ScrollY);
            }
        }

        public (int, int) ScreenCoords
        {
            get
            {
                return (ScrollX, ScrollY);
            }
        }

        public ScrollableViewport(int X, int Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }

        public void Scroll(int scrollOffsetX = 5, int scrollOffsetY = 0)
        {
            ScrollX += scrollOffsetX;
            ScrollY += scrollOffsetY;
        }

        public void ScrollCartesian(float scrollCartesianOffsetX = 5, float scrollCartesianOffsetY = 0)
        {
            var pixelScrollResult = ScreenCartesianConverter.CartesianToScreen(scrollCartesianOffsetX, scrollCartesianOffsetY, Width, Height);

            ScrollX += pixelScrollResult.Item1;
            ScrollY += pixelScrollResult.Item2;
        }

        public bool ContainsVertex(Vector4 vertexPosition)
        {
            var pixelSizeInCoords = QuadGridManager.Instance.GetPixelSizeInCoordinates();
            var x = vertexPosition.X + 1.0; // Transform From -1 to 0
            var y = -vertexPosition.Y + 1.0; // Transform From -1 to 0

            // x Tests
            var leftBound = X + ScrollX;
            var rightBound = X + Width + ScrollX;
            var xTestResult = leftBound <= vertexPosition.X && rightBound >= vertexPosition.X;

            // y Tests
            var upBound = Y;
            var downBound = Y + Height + ScrollY;
            var yTestResult = upBound <= vertexPosition.Y && downBound >= vertexPosition.Y;


            return xTestResult && yTestResult;
        }
    }
}
