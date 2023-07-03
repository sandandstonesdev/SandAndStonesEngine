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
        
        private Vector2 previousMousePos;

        public Matrix4x4 ProjectionMatrix; // From camera to projection space (output: vertex on monitor with depth)
        public Matrix4x4 ViewMatrix; // From world to camera (output: position relative to camera pov)
        public Matrix4x4 WorldMatrix; // From model to world (output: position object in world)

        public DeviceBuffer ProjectionBuffer;
        public DeviceBuffer ViewBuffer;
        public DeviceBuffer WorldBuffer;

        GameGraphicDevice graphicDevice;
        InputDevicesState inputDeviceState;

        public ResourceLayout MatricesLayout;
        public ResourceLayout WorldLayout;
        public ResourceSet MatricesSet;
        public ResourceSet WorldSet;

        int windowWidth;
        int windowHeight;

        InputMotionMapper inputMotionMapper;
        public Camera(GameGraphicDevice graphicDevice, InputDevicesState inputDeviceState)
        {
            this.inputMotionMapper = new InputMotionMapper(inputDeviceState);
            this.graphicDevice = graphicDevice;
            this.inputDeviceState = inputDeviceState;
            windowWidth = graphicDevice.GameWindow.SDLWindow.Width;
            windowHeight = graphicDevice.GameWindow.SDLWindow.Height;
            aspectRatio = windowWidth / windowHeight;
            yaw = 0.0f;
            pitch = 0.0f;
            fov = (float)(fovInDegrees * (Math.PI /180.0f));
            UpdateWorld();
            UpdateProjection();
            UpdateView();
        }

        public void DisplayMatrices()
        {
            //DebugUtilities.DebugUtilities.DisplayMatrix4x4(WorldMatrix, "WorldMatrix");
            //DebugUtilities.DebugUtilities.DisplayMatrix4x4(ProjectionMatrix, "ProjectionMatrix");
            //DebugUtilities.DebugUtilities.DisplayMatrix4x4(ViewMatrix, "ViewMatrix");
        }

        public void UpdateProjection()
        {
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fov, aspectRatio, near, far);
            DebugUtilities.DebugUtilities.DisplayMatrix4x4(ProjectionMatrix, "ProjectionMatrix");
        }

        public void UpdateWorld()
        {
            WorldMatrix = Matrix4x4.CreateWorld(-position, forward, up);
        }

        public void UpdateView()
        {
            Vector3 lookDir = GetLookDir();
            lookDirection = lookDir;
            var cameraTarget = position + lookDirection;
            ViewMatrix = Matrix4x4.CreateLookAt(position, cameraTarget, Vector3.UnitY);
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
                UpdateProjection();
                graphicDevice.ResizeWindow((uint)windowWidth, (uint)windowHeight);
            }
        }

        public void InitMatricesShaderBinding()
        {
            var factory = graphicDevice.GraphicsDevice.ResourceFactory;
            ProjectionBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            ViewBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            WorldBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));

            MatricesLayout = factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("ProjectionBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex),
                    new ResourceLayoutElementDescription("ViewBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex)));
            WorldLayout = factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("WorldBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex)));


            MatricesSet = factory.CreateResourceSet(new ResourceSetDescription(
                MatricesLayout,
                ProjectionBuffer,
                ViewBuffer));
            WorldSet = factory.CreateResourceSet(new ResourceSetDescription(
                WorldLayout,
                WorldBuffer));
        }

        public void Update(float deltaSeconds)
        {
            var motionDir = inputMotionMapper.GetMotionDir();
            if (motionDir != Vector3.Zero)
            {
                motionDir = inputMotionMapper.ApplyRotation(motionDir, yaw, pitch);
                position += motionDir * moveSpeed;
                UpdateView();
                UpdateWorld();
            }

            Vector2 yawPitchVector = inputMotionMapper.GetYawPitchVector();
            if (yawPitchVector != Vector2.Zero)
            {
                yaw += yawPitchVector.X;
                pitch += yawPitchVector.Y;
                UpdateView();
                UpdateWorld();
            }
        }

        private float Clamp(float value, float min, float max)
        {
            return value > max
                ? max
                : value < min
                    ? min
                    : value;
        }

        public void Destroy()
        {
            WorldBuffer.Dispose();
            ViewBuffer.Dispose();
            ProjectionBuffer.Dispose();
        }
    }
}
