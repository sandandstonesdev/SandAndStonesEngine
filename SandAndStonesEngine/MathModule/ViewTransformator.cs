using SandAndStonesEngine.GameCamera;
using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class ViewTransformator
    {
        InputMotionMapperBase inputMotionMapper;
        Matrices matrices;
        TransformatorData transformatorData;
        public ViewTransformator(Matrices matrices, InputMotionMapperBase inputMotionMapper, TransformatorData transformatorData)
        {
            this.matrices = matrices;
            this.inputMotionMapper = inputMotionMapper;
            this.transformatorData = transformatorData;
            matrices.UpdateView(transformatorData.Position, transformatorData.Target, transformatorData.Up);
        }

        public void Update()
        {
            var motionDir = inputMotionMapper.GetRotatedMotionDir(transformatorData.Rotation.X, transformatorData.Rotation.Y);
            if (motionDir != Vector3.Zero)
            {
                transformatorData.Position += motionDir * transformatorData.MoveSpeed;
                matrices.UpdateView(transformatorData.Position, transformatorData.Target, transformatorData.Up);
            }

            Vector2 yawPitchVector = inputMotionMapper.GetYawPitchVector();
            if (yawPitchVector != Vector2.Zero)
            {
                transformatorData.Rotation += yawPitchVector;
                matrices.UpdateView(transformatorData.Position, transformatorData.Target, transformatorData.Up);
            }
        }
    }
}
