using SandAndStonesEngine.GameCamera;
using System.Diagnostics;
using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class WorldViewTransformator
    {
        InputMotionMapperBase inputMotionMapper;
        Matrices matrices;
        TransformatorData transformatorData;
        public WorldViewTransformator(TransformatorData transformatorData, Matrices matrices, InputMotionMapperBase inputMotionMapper)
        {
            this.transformatorData = transformatorData;
            this.matrices = matrices;
            this.inputMotionMapper = inputMotionMapper;
            UpdateWorldView(transformatorData.Position, transformatorData.Forward, transformatorData.Up, transformatorData.Target);
        }

        public void Update()
        {
            var motionDir = inputMotionMapper.GetRotatedMotionDir(transformatorData.Rotation.X, transformatorData.Rotation.Y);
            if (motionDir != Vector3.Zero)
            {
                transformatorData.Position += motionDir * transformatorData.MoveSpeed;
                UpdateWorldView(transformatorData.Position, transformatorData.Forward, transformatorData.Up, transformatorData.Target);
            }

            Vector2 yawPitchVector = inputMotionMapper.GetYawPitchVector();
            if (yawPitchVector != Vector2.Zero)
            {
                transformatorData.Rotation += yawPitchVector;
                UpdateWorldView(transformatorData.Position, transformatorData.Forward, transformatorData.Up, transformatorData.Target);
            }
        }

        private void UpdateWorldView(Vector3 position, Vector3 forward, Vector3 up, Vector3 target)
        {
            matrices.UpdateView(position, target, up);
            matrices.UpdateWorld(position, forward, up);
        }
    }
}
