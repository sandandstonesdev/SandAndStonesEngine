using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameCamera;
using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class ViewTransformator
    {
        private readonly InputMotionMapperBase inputMotionMapper;
        private readonly ScrollableViewport scrollableViewport;
        public TransformatorData TransformatorData;
        public event EventHandler PositionChanged;

        public ViewTransformator(ScrollableViewport scrollableViewport, InputMotionMapperBase inputMotionMapper, TransformatorData transformatorData)
        {
            this.inputMotionMapper = inputMotionMapper;
            this.TransformatorData = transformatorData;
            this.scrollableViewport = scrollableViewport;
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
                TransformatorData.Movement = Vector3.Negate(motionDir) * TransformatorData.MoveSpeed * (float)(deltaElapsedTime / 2);
                PositionChanged?.Invoke(this, EventArgs.Empty); // This call should be moved to class managing Character
            }

            var scrollDir = inputMotionMapper.GetScrollDir();
            if (scrollDir != Vector2.Zero)
            {
                TransformatorData.ScrollSpeedPixels = 1;
                TransformatorData.ScrollMovement = scrollDir * TransformatorData.ScrollSpeedPixels * (float)(deltaElapsedTime / 2);
                scrollableViewport.Scroll((int)TransformatorData.ScrollMovement.X,
                                      (int)TransformatorData.ScrollMovement.Y);
            }
        }
    }
}
