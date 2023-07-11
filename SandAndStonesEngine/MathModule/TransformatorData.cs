using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class TransformatorData
    {
        public Vector3 Position;
        public Vector3 Forward;
        public Vector3 Up;
        public float MoveSpeed;
        public Vector2 Rotation;
        public Vector3 LookDir
        {
            get
            {
                Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, 0f);
                Vector3 lookDir = Vector3.Transform(Forward, lookRotation);
                return lookDir;
            }
        }
        public Vector3 Target
        {
            get { return Position + LookDir; }
        }

        public TransformatorData(Vector3 position, Vector3 forward, Vector3 up, Vector2 rotation, float moveSpeed)
        {
            SetDirections(position, forward, up);
            SetSpeed(moveSpeed);
            SetRotation(rotation);
        }
        public void SetDirections(Vector3 position, Vector3 forward, Vector3 up)
        {
            Position = position;
            Forward = forward;
            Up = up;
        }
        public void SetSpeed(float moveSpeed)
        {
            MoveSpeed = moveSpeed;
        }
        public void SetRotation(Vector2 rotation) // yaw/pitch
        {
            Rotation = rotation;
        }
    }
}
