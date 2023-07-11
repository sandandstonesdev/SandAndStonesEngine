using SandAndStonesEngine.GraphicAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace SandAndStonesEngine.MathModule
{
    public class Matrices
    {
        readonly GameGraphicDevice graphicDevice;

        public Matrix4x4 ProjectionMatrix; // From camera to projection space (output: vertex on monitor with depth)
        public Matrix4x4 ViewMatrix; // From world to camera (output: position relative to camera pov)
        public Matrix4x4 WorldMatrix; // From model to world (output: position object in world)



        public DeviceBuffer ProjectionBuffer;
        public DeviceBuffer ViewBuffer;
        public DeviceBuffer WorldBuffer;

        public ResourceLayout MatricesLayout;
        public ResourceLayout WorldLayout;
        public ResourceSet MatricesSet;
        public ResourceSet WorldSet;

        public Matrices(GameGraphicDevice graphicDevice)
        {
            this.graphicDevice = graphicDevice;
        }

        public void Create() // Prepare shader binding
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

        public void UpdateProjection(float fov, float aspectRatio, float near, float far)
        {
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fov, aspectRatio, near, far);
            DebugUtilities.DebugUtilities.DisplayMatrix4x4(ProjectionMatrix, "ProjectionMatrix");
        }

        public void UpdateWorld(Vector3 position, Vector3 forward, Vector3 up)
        {
            WorldMatrix = Matrix4x4.CreateWorld(-position, forward, up);
        }

        public void UpdateView(Vector3 position, Vector3 cameraTarget, Vector3 upVector)
        {
            ViewMatrix = Matrix4x4.CreateLookAt(position, cameraTarget, upVector);
        }

        public void DisplayMatrices()
        {
            //DebugUtilities.DebugUtilities.DisplayMatrix4x4(WorldMatrix, "WorldMatrix");
            //DebugUtilities.DebugUtilities.DisplayMatrix4x4(ProjectionMatrix, "ProjectionMatrix");
            //DebugUtilities.DebugUtilities.DisplayMatrix4x4(ViewMatrix, "ViewMatrix");
        }
        public void Destroy()
        {
            WorldBuffer.Dispose();
            ViewBuffer.Dispose();
            ProjectionBuffer.Dispose();
        }
    }
}
