using Microsoft.Extensions.DependencyInjection;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using System.Numerics;

namespace SandAndStonesEngine.GameCamera
{
    public class Camera
    {
        // Frustrum
        private readonly float near = 1f; // Near plane z
        private readonly float far = 100f; // Far plane z
        const float fovInDegrees = 55; // Full view
        private readonly float fov = 1f; // Field of view
        private readonly float aspectRatio; // (right - left) / (top - bottom) = right/top

        readonly Matrices matrices;
        int windowWidth;
        int windowHeight;

        Vector2 CameraPos
        {
            get
            {
                var transformator = matrices.viewTransformator;
                var data = transformator?.TransformatorData;
                if (data == null)
                {
                    return new Vector2(0, 0);
                }
                return new Vector2(data.Position.X, data.Position.Y);
            }
        }

        public Camera(int width, int height, Matrices matrices)
        {
            this.matrices = matrices;

            windowWidth = width;
            windowHeight = height;
            aspectRatio = windowWidth / windowHeight;
            fov = (float)(fovInDegrees * (Math.PI / 180.0f));
            matrices.UpdateOrtographic(windowWidth, windowHeight, near, far);
        }

        public void WindowResized(int width, int height)
        {
            bool changed = false;
            if (windowWidth != width)
            {
                windowWidth = width;
                changed = true;
            }

            if (windowHeight != height)
            {
                windowHeight = height;
                changed = true;
            }

            if (changed)
            {
                var graphicDevice = Startup.ServiceProvider.GetRequiredService<GameGraphicDevice>();
                matrices.UpdateOrtographic(windowWidth, windowHeight, near, far);
                graphicDevice.ResizeWindow((uint)windowWidth, (uint)windowHeight);
            }
        }

        public void Update(long deltaSeconds)
        {
            matrices.UpdateViewWorld();
        }
    }
}
