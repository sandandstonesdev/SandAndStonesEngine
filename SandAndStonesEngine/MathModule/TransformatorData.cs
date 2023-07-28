using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class TransformatorData
    {
        public Vector3 Position;
        public Vector3 Forward;
        public Vector3 Up;
        public float MoveSpeed;
        public Vector3 Rotation;
        public Vector3 LookDir
        {
            get
            {
                Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);
                //Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(0f, 0f, 0f);

                Vector3 lookDir = Vector3.Transform(Forward, lookRotation);
                return lookDir;
            }
        }
        public Vector3 Target
        {
            get { return Position + LookDir; }
        }

        private Vector3 relativePosition = new Vector3(-1.00f, 1.00f, 0.0f);
        public TransformatorData(Vector3 position, Vector3 forward, Vector3 up, Vector3 rotation, float moveSpeed)
        {
            //position = relativePosition + position;
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
        public void SetRotation(Vector3 rotation) // yaw/pitch
        {
            Rotation = rotation;
        }
    }
}
