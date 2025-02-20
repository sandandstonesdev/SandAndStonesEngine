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

        protected const Key scrollRightKey = Key.Right;
        protected const Key scrollLeftKey = Key.Left;
        protected const Key scrollUpKey = Key.Up;
        protected const Key scrollDownKey = Key.Down;

        protected const MouseButton mouseLeftButton = MouseButton.Left;
        protected const MouseButton mouseRightButton = MouseButton.Right;

        protected Dictionary<Key, Vector3> keyToVectorMap = new Dictionary<Key, Vector3>
            {
                { forwardKey, -Vector3.UnitZ},
                { backwardKey, Vector3.UnitZ},
                { leftKey, Vector3.UnitX},
                { rightKey, -Vector3.UnitX},
                { upKey, -Vector3.UnitY},
                { downKey, Vector3.UnitY},
            };

        protected Dictionary<Key, Vector2> keyToVectorScrollMap = new Dictionary<Key, Vector2>
        {
            { scrollRightKey, -Vector2.UnitX},
            { scrollLeftKey, Vector2.UnitX},
            { scrollUpKey, -Vector2.UnitY},
            { scrollDownKey, Vector2.UnitY}
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
        abstract public Vector3 GetYawPitchVector();
        abstract public Vector2 GetScrollDir();
    }
}