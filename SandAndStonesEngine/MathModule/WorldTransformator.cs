using SandAndStonesEngine.GameCamera;
using System.Diagnostics;
using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class WorldTransformator
    {
        InputMotionMapperBase inputMotionMapper;
        Matrices matrices;
        TransformatorData transformatorData;
        public WorldTransformator(Matrices matrices, InputMotionMapperBase inputMotionMapper, TransformatorData transformatorData)
        {
            this.matrices = matrices;
            this.inputMotionMapper = inputMotionMapper;
            this.transformatorData = transformatorData;
            matrices.UpdateWorld(transformatorData.Position, transformatorData.Forward, transformatorData.Up);
        }


        public void Update()
        {
            var motionDir = inputMotionMapper.GetRotatedMotionDir(transformatorData.Rotation.X, transformatorData.Rotation.Y);
            if (motionDir != Vector3.Zero)
            {
                transformatorData.Position += motionDir * transformatorData.MoveSpeed;
                matrices.UpdateWorld(transformatorData.Position, transformatorData.Forward, transformatorData.Up);
            }

            Vector2 yawPitchVector = inputMotionMapper.GetYawPitchVector();
            if (yawPitchVector != Vector2.Zero)
            {
                transformatorData.Rotation += yawPitchVector;
                matrices.UpdateWorld(transformatorData.Position, transformatorData.Forward, transformatorData.Up);
            }
        }
    }
}
