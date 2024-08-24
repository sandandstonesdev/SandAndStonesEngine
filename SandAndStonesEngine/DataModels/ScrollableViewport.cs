using SandAndStonesEngine.MathModule;
using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class ScrollableViewport
    {
        private readonly int X, Y, Width, Height;
        private int ScrollX = 0, ScrollY = 0;
        private const float CartesianWidth = 2.0f;
        private const float CartesianHeight = 2.0f;
        private bool ScrollProcessed = false;
        public event EventHandler ScrollChanged;
        public (float, float) CartesianCoords
        {
            get
            {
                ScrollProcessed = true;
                return ScreenCartesianConverter.ScreenToCartesianScrollMovement(ScrollX, ScrollY, Width, Height, 0);
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
            if (ScrollProcessed)
            {
                ScrollX = 0;
                ScrollY = 0;
            }

            ScrollProcessed = false;
            ScrollX += scrollOffsetX;
            ScrollY += scrollOffsetY;
            this.ScrollChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ScrollCartesian(float scrollCartesianOffsetX = 5, float scrollCartesianOffsetY = 0)
        {
            var pixelScrollResult = ScreenCartesianConverter.CartesianToScreen(scrollCartesianOffsetX, scrollCartesianOffsetY, Width, Height);

            ScrollX += pixelScrollResult.Item1;
            ScrollY += pixelScrollResult.Item2;
        }

        public bool ContainsVertex(Vector4 vertexPosition)
        {
            var coords = ScreenCartesianConverter.CartesianToScreen(vertexPosition.X, vertexPosition.Y, Width, Height);
            var x = coords.Item1;
            var y = coords.Item2;

            // x Tests
            var leftBound = X + Math.Abs(ScrollX);
            var rightBound = X + Width + Math.Abs(ScrollX);
            var xTestResult = leftBound <= x && rightBound >= x;

            // y Tests
            var upBound = Y + Math.Abs(ScrollY);
            var downBound = Y + Height + Math.Abs(ScrollY);
            var yTestResult = upBound <= y && downBound >= y;


            return xTestResult && yTestResult;
        }
    }
}
