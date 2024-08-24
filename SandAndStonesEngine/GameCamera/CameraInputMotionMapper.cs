using SandAndStonesEngine.GameInput;
using System.Numerics;

namespace SandAndStonesEngine.GameCamera
{
    public class CameraInputMotionMapper : InputMotionMapperBase
    {
        public CameraInputMotionMapper(InputDevicesState inputDevicesState) : base(inputDevicesState)
        {
        }

        public override Vector3 GetMotionDir()
        {
            Vector3 motionDir = Vector3.Zero;

            foreach (var keyToCheck in keyToVectorMap)
            {
                if (inputDevicesState.GetKey(keyToCheck.Key))
                    motionDir += keyToCheck.Value;
            }

            return motionDir;
        }

        public override Vector3 GetRotatedMotionDir(float yaw, float pitch)
        {
            Vector3 motionDir = GetMotionDir();
            if (motionDir != Vector3.Zero)
            {
                motionDir = ApplyRotationOnMotionDir(motionDir, yaw, pitch);
            }
            return motionDir;
        }

        public override Vector3 GetYawPitchVector()
        {
            Vector3 mouseRotationResult = Vector3.Zero;
            foreach (var mouseButtonToCheck in mouseButtonPressedMap)
            {
                mouseButtonPressedMap[mouseButtonToCheck.Key] = inputDevicesState.GetMouseButton(mouseButtonToCheck.Key);
            }

            if (mouseButtonPressedMap.Any(e => e.Value))
            {
                Vector2 mouseDelta = inputDevicesState.MouseDelta;
                mouseRotationResult.X = Math.Clamp(-mouseDelta.X * 0.01f, -1.55f, 1.55f);
                mouseRotationResult.Y = Math.Clamp(-mouseDelta.Y * 0.01f, -1.55f, 1.55f);
            }

            return mouseRotationResult;
        }

        private Vector3 ApplyRotationOnMotionDir(Vector3 motionDir, float yaw, float pitch)
        {
            Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0f);
            Vector3 rotatedMotionDir = Vector3.Transform(motionDir, lookRotation);
            return rotatedMotionDir;
        }

        public override Vector2 GetScrollDir()
        {
            Vector2 scrollDir = Vector2.Zero;

            foreach (var scrollKeyToCheck in keyToVectorScrollMap)
            {
                if (inputDevicesState.GetKey(scrollKeyToCheck.Key))
                    scrollDir = scrollKeyToCheck.Value;
            }

            return scrollDir;
        }
    }
}
