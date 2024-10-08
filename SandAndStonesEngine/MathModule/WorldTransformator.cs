﻿using SandAndStonesEngine.GameCamera;
using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class WorldTransformator
    {
        private readonly InputMotionMapperBase inputMotionMapper;
        public readonly TransformatorData TransformatorData;

        public WorldTransformator(InputMotionMapperBase inputMotionMapper, TransformatorData transformatorData)
        {
            this.inputMotionMapper = inputMotionMapper;
            this.TransformatorData = transformatorData;
        }

        public void Update(double deltaElapsedTime)
        {
            Vector3 yawPitchVector = inputMotionMapper.GetYawPitchVector();
            if (yawPitchVector != Vector3.Zero)
            {
                TransformatorData.Rotation += yawPitchVector;
            }

            var motionDir = inputMotionMapper.GetRotatedMotionDir(TransformatorData.Rotation.X, TransformatorData.Rotation.Y);
            if (motionDir != Vector3.Zero)
            {
                //TransformatorData.Position += motionDir * TransformatorData.MoveSpeed;
            }
        }
    }
}
