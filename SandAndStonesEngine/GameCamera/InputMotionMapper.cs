using System.Numerics;
using Veldrid;
using SandAndStonesEngine.GameInput;

namespace SandAndStonesEngine.GameCamera
{
    public class InputMotionMapper
    {
        const Key forwardKey = Key.W;
        const Key backwardKey = Key.S;
        const Key leftKey = Key.A;
        const Key rightKey = Key.D;
        const Key upKey = Key.Q;
        const Key downKey = Key.E;

        const MouseButton mouseLeftButton = MouseButton.Left;
        const MouseButton mouseRightButton = MouseButton.Right;

        Dictionary<Key, Vector3> keyToVectorMap = new Dictionary<Key, Vector3>
            {
                { forwardKey, -Vector3.UnitZ},
                { backwardKey, Vector3.UnitZ},
                { leftKey, -Vector3.UnitX},
                { rightKey, Vector3.UnitX},
                { upKey, -Vector3.UnitY},
                { downKey, Vector3.UnitY}
            };

        Dictionary<MouseButton, bool> mouseButtonPressedMap = new Dictionary<MouseButton, bool>
            {
                { mouseLeftButton, false},
                { mouseRightButton, false},
            };

        private InputDevicesState inputDevicesState;
        public InputMotionMapper(InputDevicesState inputDevicesState)
        {
            this.inputDevicesState = inputDevicesState;
        }

        public Vector3 GetMotionDir()
        {
            Vector3 motionDir = Vector3.Zero;
            

            foreach (var keyToCheck in keyToVectorMap)
            {
                if (inputDevicesState.GetKey(keyToCheck.Key))
                    motionDir += keyToCheck.Value;
            }

            return motionDir;
        }

        public Vector3 ApplyRotation(Vector3 motionDir, float yaw, float pitch)
        {
            Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0f);
            Vector3 rotatedMotionDir = Vector3.Transform(motionDir, lookRotation);
            return rotatedMotionDir;
        }

        public Vector2 GetYawPitchVector()
        {
            Vector2 mouseRotationResult = new Vector2();
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
    }
}
