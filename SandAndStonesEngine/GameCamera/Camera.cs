﻿using System.Numerics;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.GameFactories;
using SandAndStonesEngine.DataModels;

namespace SandAndStonesEngine.GameCamera
{
    public class Camera
    {
        // Frustrum
        private float near = 1f; // Near plane z
        private float far = 100f; // Far plane z
        const float fovInDegrees = 55; // Full view
        private float fov = 1f; // Field of view
        private float aspectRatio; // (right - left) / (top - bottom) = right/top

        readonly Matrices matrices;
        int windowWidth;
        int windowHeight;

        Vector2 CameraPos
        {
            get
            {
                var transformator = matrices.viewTransformator;
                var data = transformator?.TransformatorData;
                if (data==null)
                {
                    return new Vector2(0, 0);
                }
                return new Vector2(data.Position.X, data.Position.Y);
            }
        }

        readonly ScrollableViewport scrollableViewport;

        public Camera(Matrices matrices, ScrollableViewport scrollableViewport)
        {
            this.matrices = matrices;
            this.scrollableViewport = scrollableViewport;

            windowWidth = GameWindow.Instance.SDLWindow.Width;
            windowHeight = GameWindow.Instance.SDLWindow.Height;
            aspectRatio = windowWidth / windowHeight;
            fov = (float)(fovInDegrees * (Math.PI /180.0f));
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
                var graphicDevice = Factory.Instance.GetGameGraphicDevice();
                matrices.UpdateOrtographic(windowWidth, windowHeight, near, far);
                graphicDevice.ResizeWindow((uint)windowWidth, (uint)windowHeight);
            }
        }

        public void Update(long deltaSeconds)
        {
            matrices.UpdateViewWorld();
        }

        public void Dispose()
        {

        }
    }
}
