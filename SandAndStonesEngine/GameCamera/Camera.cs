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

        private Vector3 forward { get { return GetLookDir(); } }
        private Vector3 up = new Vector3(0, 1 , 0);

        private float aspectRatio; // (right - left) / (top - bottom) = right/top

        // Camera info
        private Vector3 position = new Vector3(0, 0, 1.0f);
        private Vector3 lookDirection = new Vector3(0f, 0f, -1f);
        private float moveSpeed = 0.002f;

        private float yaw = 0.0f;
        private float pitch = 0.0f;

        GameGraphicDevice graphicDevice;
        readonly InputDevicesState inputDeviceState;
        readonly Matrices matrices;

        int windowWidth;
        int windowHeight;

        CameraInputMotionMapper inputMotionMapper;
        public Camera(GameGraphicDevice graphicDevice, InputDevicesState inputDeviceState, Matrices matrices)
        {
            this.matrices = matrices;
            this.inputMotionMapper = new CameraInputMotionMapper(inputDeviceState);
            this.graphicDevice = graphicDevice;
            this.inputDeviceState = inputDeviceState;
            windowWidth = graphicDevice.GameWindow.SDLWindow.Width;
            windowHeight = graphicDevice.GameWindow.SDLWindow.Height;
            aspectRatio = windowWidth / windowHeight;
            yaw = 0.0f;
            pitch = 0.0f;
            fov = (float)(fovInDegrees * (Math.PI /180.0f));
            matrices.UpdateWorld(position, forward, up);
            matrices.UpdateProjection(fov, aspectRatio, near, far);
            matrices.UpdateView(position, GetCameraTarget(), up);
        }

        private Vector3 GetCameraTarget()
        {
            Vector3 lookDir = GetLookDir();
            lookDirection = lookDir;
            var cameraTarget = position + lookDirection;
            return cameraTarget;
        }

        private Vector3 GetLookDir()
        {
            Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0f);
            Vector3 lookDir = Vector3.Transform(-Vector3.UnitZ, lookRotation);
            return lookDir;
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
            var motionDir = inputMotionMapper.GetRotatedMotionDir(yaw, pitch);
            if (motionDir != Vector3.Zero)
            {
                position += motionDir * moveSpeed;
                matrices.UpdateView(position, GetCameraTarget(), up);
                matrices.UpdateWorld(position, forward, up);
            }
            
            Vector2 yawPitchVector = inputMotionMapper.GetYawPitchVector();
            if (yawPitchVector != Vector2.Zero)
            {
                yaw += yawPitchVector.X;
                pitch += yawPitchVector.Y;
                matrices.UpdateView(position, GetCameraTarget(), up);
                matrices.UpdateWorld(position, forward, up);
            }
        }

        public void Destroy()
        {

        }
    }
}
