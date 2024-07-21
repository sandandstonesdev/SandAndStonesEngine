using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.GameInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.GameCamera
{
    public class QuadInputMotionMapper : InputMotionMapperBase
    {
        public QuadInputMotionMapper(InputDevicesState inputDevicesState) : base(inputDevicesState)
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

        private Vector3 ApplyRotationOnMotionDir(Vector3 motionDir, float yaw, float pitch)
        {
            Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0f);
            Vector3 rotatedMotionDir = Vector3.Transform(motionDir, lookRotation);
            return rotatedMotionDir;
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

        public override Vector2 GetScrollDir()
        {
            return Vector2.Zero;
        }
    }
}
