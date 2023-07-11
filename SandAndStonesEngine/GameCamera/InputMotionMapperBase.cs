using SandAndStonesEngine.GameInput;
using System.Numerics;
using Veldrid;

namespace SandAndStonesEngine.GameCamera
{
    public abstract class InputMotionMapperBase
    {
        protected const Key forwardKey = Key.W;
        protected const Key backwardKey = Key.S;
        protected const Key leftKey = Key.A;
        protected const Key rightKey = Key.D;
        protected const Key upKey = Key.Q;
        protected const Key downKey = Key.E;

        protected const MouseButton mouseLeftButton = MouseButton.Left;
        protected const MouseButton mouseRightButton = MouseButton.Right;

        protected Dictionary<Key, Vector3> keyToVectorMap = new Dictionary<Key, Vector3>
            {
                { forwardKey, -Vector3.UnitZ},
                { backwardKey, Vector3.UnitZ},
                { leftKey, -Vector3.UnitX},
                { rightKey, Vector3.UnitX},
                { upKey, -Vector3.UnitY},
                { downKey, Vector3.UnitY}
            };

        protected Dictionary<MouseButton, bool> mouseButtonPressedMap = new Dictionary<MouseButton, bool>
            {
                { mouseLeftButton, false},
                { mouseRightButton, false},
            };

        protected InputDevicesState inputDevicesState;

        public InputMotionMapperBase(InputDevicesState inputDevicesState)
        {
            this.inputDevicesState = inputDevicesState;
        }
        abstract public Vector3 GetMotionDir();
        abstract public Vector3 GetRotatedMotionDir(float yaw, float pitch);
        abstract public Vector2 GetYawPitchVector();
    }
}