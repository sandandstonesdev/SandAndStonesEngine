using SandAndStonesEngine.DataModels.ScreenDivisions;
using SandAndStonesEngine.MathModule;
using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class ScrollableViewport
    {
        private readonly int X, Y;
        private int ScrollX = 0, ScrollY = 0;
        private bool ScrollProcessed = false;
        public event EventHandler ScrollChanged;
        public (float, float) CartesianCoords
        {
            get
            {
                ScrollProcessed = true;
                return ScreenCartesianConverter.ScreenToCartesianScrollMovement(
                    ScrollX, ScrollY, ScreenQuadCalculator.ScreenWidth, ScreenQuadCalculator.ScreenHeight, 0);
            }
        }

        public (int, int) ScreenCoords
        {
            get
            {
                return (ScrollX, ScrollY);
            }
        }

        ScreenQuadCalculator ScreenQuadCalculator;

        public ScrollableViewport(int X, int Y, int Width, int Height, ScreenQuadCalculator screenQuadCalculator)
        {
            this.X = X;
            this.Y = Y;
            this.ScreenQuadCalculator = screenQuadCalculator;
        }

        private bool ScrollSampleMoved(int scrollX, int scrollY, ScreenQuadCalculator screenQuadCalculator)
        {
            var deltaX = scrollX / screenQuadCalculator.ScreenWidth;
            var deltaY = scrollY / screenQuadCalculator.ScreenHeight;
            return true;
        }

        public void Scroll(int scrollOffsetX = 5, int scrollOffsetY = 0)
        {
            if (ScrollProcessed)
            {
                ScrollX = 0;
                ScrollY = 0;
            }

            ScrollProcessed = false;
            ScrollX += scrollOffsetX;
            ScrollY += scrollOffsetY;
            this.ScrollChanged?.Invoke(this, EventArgs.Empty);

            //this.ScrollChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ScrollCartesian(float scrollCartesianOffsetX = 5, float scrollCartesianOffsetY = 0)
        {
            var pixelScrollResult = ScreenCartesianConverter.CartesianToScreen(scrollCartesianOffsetX, scrollCartesianOffsetY, ScreenQuadCalculator.ScreenWidth, ScreenQuadCalculator.ScreenHeight);

            ScrollX += pixelScrollResult.Item1;
            ScrollY += pixelScrollResult.Item2;
        }

        public bool ContainsVertex(Vector4 vertexPosition)
        {
            var coords = ScreenCartesianConverter.CartesianToScreen(vertexPosition.X, vertexPosition.Y, ScreenQuadCalculator.ScreenWidth, ScreenQuadCalculator.ScreenHeight);
            var x = coords.Item1;
            var y = coords.Item2;

            // x Tests
            var leftBound = X + Math.Abs(ScrollX);
            var rightBound = X + ScreenQuadCalculator.ScreenWidth + Math.Abs(ScrollX);
            var xTestResult = leftBound <= x && rightBound >= x;

            // y Tests
            var upBound = Y + Math.Abs(ScrollY);
            var downBound = Y + ScreenQuadCalculator.ScreenHeight + Math.Abs(ScrollY);
            var yTestResult = upBound <= y && downBound >= y;


            return xTestResult && yTestResult;
        }
    }
}
