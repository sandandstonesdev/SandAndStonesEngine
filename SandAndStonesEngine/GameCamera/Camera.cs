using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Veldrid;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.GameInput;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Vulkan.Xlib;
using System.Runtime.CompilerServices;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.GameFactories;

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

        GameGraphicDevice graphicDevice;
        readonly InputDevicesState inputDeviceState;
        readonly Matrices matrices;

        int windowWidth;
        int windowHeight;

        CameraInputMotionMapper inputMotionMapper;
        ViewTransformator viewTransformator;

        public Camera(InputDevicesState inputDeviceState, Matrices matrices, TransformatorData transformatorData)
        {
            this.matrices = matrices;
            this.inputDeviceState = inputDeviceState;
            this.inputMotionMapper = new CameraInputMotionMapper(inputDeviceState);
            this.viewTransformator = new ViewTransformator(matrices, inputMotionMapper, transformatorData);
            
            windowWidth = GameWindow.Instance.SDLWindow.Width;
            windowHeight = GameWindow.Instance.SDLWindow.Height;
            aspectRatio = windowWidth / windowHeight;
            fov = (float)(fovInDegrees * (Math.PI /180.0f));
            //matrices.UpdateProjection(fov, aspectRatio, near, far);
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
                changed = true;
                windowHeight = height;
            }
            
            if (changed)
            {
                var graphicDevice = Factory.Instance.GetGameGraphicDevice();
                graphicDevice.ResizeWindow((uint)windowWidth, (uint)windowHeight);
                matrices.UpdateOrtographic(windowWidth, windowHeight, near, far);
                //matrices.UpdateProjection(fov, aspectRatio, near, far);
            }
        }

        public void Update(float deltaSeconds)
        {
            viewTransformator.Update();
        }

        public void Dispose()
        {

        }
    }
}
