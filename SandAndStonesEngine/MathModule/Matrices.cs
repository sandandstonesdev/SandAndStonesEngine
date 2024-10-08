﻿using SandAndStonesEngine.GraphicAbstractions;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.MathModule
{
    public class Matrices : IDisposable
    {
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
        private bool disposedValue;
        public WorldTransformator worldTransformator;
        public ViewTransformator viewTransformator;
        public readonly GameGraphicDevice gameGraphicDevice;

        public Matrices(GameGraphicDevice gameGraphicDevice, WorldTransformator worldTransformator, ViewTransformator viewTransformator)
        {
            this.gameGraphicDevice = gameGraphicDevice;
            this.worldTransformator = worldTransformator;
            this.viewTransformator = viewTransformator;
            UpdateViewWorld();
        }

        public void Init() // Prepare shader binding
        {
            var factory = gameGraphicDevice.ResourceFactory;

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

        public void UpdateView()
        {
            var viewTransformatorData = viewTransformator.TransformatorData;
            ViewMatrix = Matrix4x4.CreateLookAt(viewTransformatorData.Position, viewTransformatorData.Target, viewTransformatorData.Up);
        }

        public void UpdateWorld()
        {
            var worldTransformatorData = worldTransformator.TransformatorData;
            WorldMatrix = Matrix4x4.CreateWorld(-worldTransformatorData.Position, worldTransformatorData.Forward, worldTransformatorData.Up);
        }

        public void UpdateViewWorld()
        {
            UpdateWorld();
            UpdateView();
        }

        public void UpdateOrtographic(float width, float height, float near, float far)
        {
            float w = (width / height) * 2.0f;
            float h = (height / width) * 2.0f;
            ProjectionMatrix = Matrix4x4.CreateOrthographic(w, h, near, far);
        }

        public void UpdateProjection(float fov, float aspectRatio, float near, float far)
        {
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(fov, aspectRatio, near, far);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                WorldBuffer.Dispose();
                ViewBuffer.Dispose();
                ProjectionBuffer.Dispose();
                MatricesLayout.Dispose();
                WorldLayout.Dispose();
                MatricesSet.Dispose();
                WorldSet.Dispose();
                disposedValue = true;
            }
        }

        ~Matrices()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
