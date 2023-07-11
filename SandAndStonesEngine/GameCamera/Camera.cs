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

        // Camera info
        private Vector3 lookDirection = new Vector3(0f, 0f, -1f);

        GameGraphicDevice graphicDevice;
        readonly InputDevicesState inputDeviceState;
        readonly Matrices matrices;

        int windowWidth;
        int windowHeight;

        CameraInputMotionMapper inputMotionMapper;
        WorldViewTransformator worldViewTransformator;

        public Camera(GameGraphicDevice graphicDevice, InputDevicesState inputDeviceState, Matrices matrices)
        {
            this.graphicDevice = graphicDevice;
            this.matrices = matrices;
            this.inputDeviceState = inputDeviceState;
            this.inputMotionMapper = new CameraInputMotionMapper(inputDeviceState);
            var transformatorData = new TransformatorData(new Vector3(0, 0, 1.0f), new Vector3(0, 0, -1), new Vector3(0, 1, 0), new Vector2(0, 0), 0.002f);
            this.worldViewTransformator = new WorldViewTransformator(transformatorData, matrices, inputMotionMapper);
            
            windowWidth = graphicDevice.GameWindow.SDLWindow.Width;
            windowHeight = graphicDevice.GameWindow.SDLWindow.Height;
            aspectRatio = windowWidth / windowHeight;
            fov = (float)(fovInDegrees * (Math.PI /180.0f));
            matrices.UpdateProjection(fov, aspectRatio, near, far);
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
                matrices.UpdateProjection(fov, aspectRatio, near, far);
                graphicDevice.ResizeWindow((uint)windowWidth, (uint)windowHeight);
            }
        }

        public void Update(float deltaSeconds)
        {
            worldViewTransformator.Update();
        }

        public void Destroy()
        {

        }
    }
}
